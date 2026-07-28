#!/usr/bin/env python3
import ast
import html
import json
import re
import sys
from collections import defaultdict
from pathlib import Path

LANGS = ("az", "en", "ru")
FALLBACK_IMAGE = "https://ask.org.az/wp-content/uploads/2025/08/ASK-logo-600x400.jpg"


def iter_insert_tuples(sql, table):
    pattern = re.compile(r"INSERT INTO `" + re.escape(table) + r"` VALUES\n", re.M)
    pos = 0
    while True:
        match = pattern.search(sql, pos)
        if not match:
            return

        i = match.end()
        depth = 0
        in_string = False
        escaped = False
        current = []

        while i < len(sql):
            ch = sql[i]
            if in_string:
                current.append(ch)
                if escaped:
                    escaped = False
                elif ch == "\\":
                    escaped = True
                elif ch == "'":
                    in_string = False
            else:
                if ch == "'":
                    in_string = True
                    current.append(ch)
                elif ch == "(":
                    if depth == 0:
                        current = []
                    else:
                        current.append(ch)
                    depth += 1
                elif ch == ")":
                    depth -= 1
                    if depth == 0:
                        yield "(" + "".join(current) + ")"
                    else:
                        current.append(ch)
                elif depth > 0:
                    current.append(ch)
                elif ch == ";":
                    pos = i + 1
                    break
            i += 1


def parse_tuple(raw):
    return ast.literal_eval(raw.replace("NULL", "None"))


def clean(value):
    if value is None:
        return ""
    value = html.unescape(str(value))
    value = value.replace("\r\n", "\n").replace("\r", "\n").strip()
    return re.sub(r"\n{3,}", "\n\n", value)


def short(value, limit=500):
    value = clean(value)
    return value[:limit].rstrip() if len(value) > limit else value


def first_not_empty(*values):
    for value in values:
        value = clean(value)
        if value:
            return value
    return ""


def media(url, public_id):
    url = clean(url) or FALLBACK_IMAGE
    public_id = clean(public_id) or "fallback"
    return {"url": url, "publicId": f"wordpress/{public_id}"}


def image_sources(content):
    result = []
    for match in re.finditer(r"<img[^>]+src=[\"']([^\"']+)[\"']", content or "", flags=re.I):
        url = html.unescape(match.group(1)).strip()
        if url and url not in result:
            result.append(url)
    return result


def serialized_strings(value):
    return [html.unescape(x) for x in re.findall(r's:\d+:"(.*?)";', value or "", flags=re.S)]


def serialized_ids(value):
    return [int(x) for x in re.findall(r's:\d+:"(\d+)";', value or "")]


def split_lines(value):
    return [line.strip() for line in clean(value).splitlines() if line.strip()]


def parse_district(value):
    text = re.sub(r"^\s*\d+\.\s*", "", clean(value))
    district, rest = (text.split(" - ", 1) + [""])[:2] if " - " in text else (text, "")
    full_name, company = (rest.split(" / ", 1) + [""])[:2] if " / " in rest else (rest, "")
    return district.strip(), full_name.strip(), company.strip()


def load_backup(path):
    sql = path.read_text(errors="replace")

    posts = {}
    for raw in iter_insert_tuples(sql, "wp_posts"):
        row = parse_tuple(raw)
        posts[int(row[0])] = {
            "id": int(row[0]),
            "date": clean(row[2]),
            "content": clean(row[4]),
            "title": clean(row[5]),
            "status": clean(row[7]),
            "slug": clean(row[11]),
            "parent": int(row[17]),
            "guid": clean(row[18]),
            "type": clean(row[20]),
            "mime": clean(row[21]),
        }

    meta = defaultdict(dict)
    for raw in iter_insert_tuples(sql, "wp_postmeta"):
        row = parse_tuple(raw)
        meta[int(row[1])][clean(row[2])] = row[3]

    terms = {}
    for raw in iter_insert_tuples(sql, "wp_terms"):
        row = parse_tuple(raw)
        terms[int(row[0])] = {"name": clean(row[1]), "slug": clean(row[2])}

    taxonomies = {}
    for raw in iter_insert_tuples(sql, "wp_term_taxonomy"):
        row = parse_tuple(raw)
        taxonomies[int(row[0])] = {"termId": int(row[1]), "taxonomy": clean(row[2])}

    relationships = defaultdict(list)
    for raw in iter_insert_tuples(sql, "wp_term_relationships"):
        row = parse_tuple(raw)
        relationships[int(row[0])].append(int(row[1]))

    translations = {}
    groups = defaultdict(dict)
    for raw in iter_insert_tuples(sql, "wp_icl_translations"):
        row = parse_tuple(raw)
        element_type = clean(row[1])
        element_id = row[2]
        if element_id is None:
            continue
        element_id = int(element_id)
        trid = int(row[3])
        lang = clean(row[4])
        translations[(element_type, element_id)] = {"trid": trid, "lang": lang}
        groups[(element_type, trid)][lang] = element_id

    return posts, meta, terms, taxonomies, relationships, translations, groups


def categories(pid, terms, taxonomies, relationships, taxonomy=None):
    names = []
    slugs = []
    for tt_id in relationships.get(pid, []):
        tax = taxonomies.get(tt_id)
        if not tax or (taxonomy and tax["taxonomy"] != taxonomy):
            continue
        term = terms.get(tax["termId"])
        if term:
            names.append(term["name"])
            slugs.append(term["slug"])
    return names, slugs


def attachment_url(posts, attachment_id):
    try:
        attachment_id = int(attachment_id)
    except (TypeError, ValueError):
        return ""
    return posts.get(attachment_id, {}).get("guid", "")


def localized_triplet(ids_by_lang, posts, key):
    values = {lang: clean(posts.get(ids_by_lang.get(lang, 0), {}).get(key, "")) for lang in LANGS}
    fallback = first_not_empty(values.get("az"), values.get("en"), values.get("ru"))
    return {lang: values.get(lang) or fallback for lang in LANGS}


def localized_meta_triplet(source, prefixes, fields):
    result = {}
    for lang, prefix in prefixes.items():
        result[lang] = {field: clean(source.get(f"{prefix}_{field}", "")) for field in fields}
    fallback_lang = next((lang for lang in LANGS if any(result[lang].values())), "az")
    for lang in LANGS:
        for field in fields:
            result[lang][field] = result[lang][field] or result[fallback_lang][field]
    return result


def build_post_records(posts, meta, terms, taxonomies, relationships, groups):
    news = []
    announcements = []
    seen = set()

    for (element_type, trid), ids_by_lang in groups.items():
        if element_type != "post_post" or trid in seen:
            continue
        seen.add(trid)
        ids = [pid for pid in ids_by_lang.values() if posts.get(pid, {}).get("status") == "publish"]
        if not ids:
            continue

        names = []
        for pid in ids:
            names.extend(categories(pid, terms, taxonomies, relationships, "category")[0])
        is_announcement = any(name in ("Elanlar", "Объявления") for name in names)
        is_news = any(name in ("Xəbərlər", "News", "Новости") for name in names)
        if not is_news and not is_announcement:
            continue

        ref_id = ids_by_lang.get("az") or ids[0]
        ref = posts[ref_id]
        titles = localized_triplet(ids_by_lang, posts, "title")
        contents = localized_triplet(ids_by_lang, posts, "content")
        thumb_id = meta[ref_id].get("_thumbnail_id")
        cover_url = attachment_url(posts, thumb_id) or first_not_empty(*image_sources(ref["content"]), FALLBACK_IMAGE)
        image_urls = []
        for pid in ids:
            for url in image_sources(posts[pid]["content"]):
                if url != cover_url and url not in image_urls:
                    image_urls.append(url)

        record = {
            "sourceId": ref_id,
            "created": ref["date"],
            "titleAz": short(titles["az"]),
            "titleEn": short(titles["en"]),
            "titleRu": short(titles["ru"]),
            "textAz": contents["az"],
            "textEn": contents["en"],
            "textRu": contents["ru"],
            "titleImage": media(cover_url, f"post/{ref_id}/title"),
            "images": [media(url, f"post/{ref_id}/image/{index}") for index, url in enumerate(image_urls)],
        }
        (announcements if is_announcement else news).append(record)

    return news, announcements


def build_event_records(posts, meta, terms, taxonomies, relationships, groups):
    exhibitions = []
    trainings = []
    business_forums = []
    press_release_news = []

    for (element_type, trid), ids_by_lang in groups.items():
        if element_type != "post_tribe_events":
            continue
        ids = [pid for pid in ids_by_lang.values() if posts.get(pid, {}).get("status") == "publish"]
        if not ids:
            continue
        ref_id = ids_by_lang.get("az") or ids[0]
        names = []
        slugs = []
        for pid in ids:
            c_names, c_slugs = categories(pid, terms, taxonomies, relationships, "tribe_events_cat")
            names.extend(c_names)
            slugs.extend(c_slugs)
        category_text = " ".join(names + slugs).lower()

        titles = localized_triplet(ids_by_lang, posts, "title")
        contents = localized_triplet(ids_by_lang, posts, "content")
        ref = posts[ref_id]
        start = clean(meta[ref_id].get("_EventStartDate")) or ref["date"]
        end = clean(meta[ref_id].get("_EventEndDate")) or start
        cover_url = attachment_url(posts, meta[ref_id].get("_thumbnail_id")) or first_not_empty(*image_sources(ref["content"]), FALLBACK_IMAGE)
        common = {
            "sourceId": ref_id,
            "created": ref["date"],
            "startDate": start,
            "endDate": end,
            "titleAz": short(titles["az"]),
            "titleEn": short(titles["en"]),
            "titleRu": short(titles["ru"]),
            "textAz": contents["az"],
            "textEn": contents["en"],
            "textRu": contents["ru"],
            "titleImage": media(cover_url, f"event/{ref_id}/title"),
        }

        if "biznes-forum" in category_text or "business-forum" in category_text:
            item = dict(common)
            item["detailImage"] = media(cover_url, f"event/{ref_id}/detail")
            business_forums.append(item)
        elif "sergi" in category_text or "exhibition" in category_text or "выстав" in category_text:
            exhibitions.append(common)
        elif "seminar" in category_text or "training" in category_text or "трен" in category_text:
            trainings.append(common)
        elif "press" in category_text or "reliz" in category_text:
            press_release_news.append({
                "sourceId": ref_id,
                "created": ref["date"],
                "titleAz": common["titleAz"],
                "titleEn": common["titleEn"],
                "titleRu": common["titleRu"],
                "textAz": common["textAz"],
                "textEn": common["textEn"],
                "textRu": common["textRu"],
                "titleImage": common["titleImage"],
                "images": [],
            })

    return exhibitions, trainings, business_forums, press_release_news


def build_page_records(posts, meta):
    page_meta = meta

    directors = []
    director_count = int(clean(page_meta[25].get("icra")) or 0)
    for i in range(director_count):
        az = {
            "name": page_meta[25].get(f"icra_{i}_ad"),
            "duty": page_meta[25].get(f"icra_{i}_vezife"),
            "department": page_meta[25].get(f"icra_{i}_sobə"),
        }
        en = {
            "name": page_meta[25].get(f"icraeng_{i}_adeng"),
            "duty": page_meta[25].get(f"icraeng_{i}_vezifeeng"),
            "department": page_meta[25].get(f"icraeng_{i}_sobəeng"),
        }
        ru = {
            "name": page_meta[25].get(f"icrarus_{i}_adrus"),
            "duty": page_meta[25].get(f"icrarus_{i}_veziferus"),
            "department": page_meta[25].get(f"icrarus_{i}_sobərus"),
        }
        name = first_not_empty(az["name"], en["name"], ru["name"])
        duty = first_not_empty(az["duty"], en["duty"], ru["duty"])
        if not name or not duty:
            continue
        image_id = first_not_empty(page_meta[25].get(f"icra_{i}_sekil"), page_meta[25].get(f"icraeng_{i}_sekileng"))
        image_url = attachment_url(posts, image_id) or FALLBACK_IMAGE
        directors.append({
            "sourceId": f"page25/icra/{i}",
            "fullNameAz": short(az["name"] or name, 200),
            "fullNameEn": short(en["name"] or name, 200),
            "fullNameRu": short(ru["name"] or name, 200),
            "dutyAz": short(az["duty"] or duty, 200),
            "dutyEn": short(en["duty"] or duty, 200),
            "dutyRu": short(ru["duty"] or duty, 200),
            "departmentAz": short(az["department"] or "", 200),
            "departmentEn": short(en["department"] or az["department"] or "", 200),
            "departmentRu": short(ru["department"] or az["department"] or "", 200),
            "phoneNumber": short(first_not_empty(page_meta[25].get(f"icra_{i}_telefon"), page_meta[25].get(f"icraeng_{i}_telefoneng")), 50),
            "email": short(first_not_empty(page_meta[25].get(f"icra_{i}_mailler"), page_meta[25].get(f"icra_{i}_mail"), page_meta[25].get(f"icraeng_{i}_maillereng")), 256),
            "image": media(image_url, f"page25/icra/{i}/image"),
        })

    management = []
    management_count = int(clean(page_meta[23].get("idare")) or 0)
    for i in range(management_count):
        name = clean(page_meta[23].get(f"idare_{i}_adsoyad"))
        company = clean(page_meta[23].get(f"idare_{i}_sirket"))
        if name:
            management.append({
                "sourceId": f"page23/idare/{i}",
                "fullNameAz": short(name, 200),
                "fullNameEn": short(name, 200),
                "fullNameRu": short(name, 200),
                "companyAz": short(company or "-", 200),
                "companyEn": short(company or "-", 200),
                "companyRu": short(company or "-", 200),
            })

    committees = []
    for i in range(100):
        name = clean(page_meta[27].get(f"komissiyalar_{i}_adi"))
        chairman = clean(page_meta[27].get(f"komissiyalar_{i}_komissiyani_sədri"))
        if not name:
            continue
        committees.append({
            "sourceId": f"page27/komissiyalar/{i}",
            "nameAz": short(name, 200),
            "nameEn": short(name, 200),
            "nameRu": short(name, 200),
            "chairmanAz": short(chairman or "-", 200),
            "chairmanEn": short(chairman or "-", 200),
            "chairmanRu": short(chairman or "-", 200),
            "vicePresidentAz": "-",
            "vicePresidentEn": "-",
            "vicePresidentRu": "-",
        })

    districts = []
    district_count = int(clean(page_meta[29].get("rayon")) or 0)
    for i in range(district_count):
        az_district, az_name, az_company = parse_district(page_meta[29].get(f"rayon_{i}_rayonadi"))
        ru_district, ru_name, ru_company = parse_district(page_meta[29].get(f"rayonrus_{i}_rayonadirus"))
        if not az_district and not ru_district:
            continue
        districts.append({
            "sourceId": f"page29/rayon/{i}",
            "districtAz": short(az_district or ru_district, 200),
            "districtEn": short(az_district or ru_district, 200),
            "districtRu": short(ru_district or az_district, 200),
            "fullNameAz": short(az_name or ru_name or "-", 200),
            "fullNameEn": short(az_name or ru_name or "-", 200),
            "fullNameRu": short(ru_name or az_name or "-", 200),
            "companyAz": short(az_company or "-", 200),
            "companyEn": short(az_company or "-", 200),
            "companyRu": short(ru_company or az_company or "-", 200),
        })

    foreign = []
    table_by_lang = {
        "az": serialized_strings(page_meta[31].get("xarici")),
        "en": serialized_strings(page_meta[31].get("xaricieng")),
        "ru": serialized_strings(page_meta[31].get("xaricirus")),
    }
    rows_by_lang = {}
    for lang, values in table_by_lang.items():
        data = [value for value in values if value not in {"p", "o", "uh", "c", "h", "b", "acftf", "v", "1.3.5", ""}]
        header_markers = {"S/S", "No", "Xarici ölkənin adı", "Country", "Название зарубежной страны", "Email", "e-mail ünvanı"}
        while data and data[0] not in {"1", "Kingdom of Spain", "İspaniya  Krallığı", "Королевство Испания"}:
            data.pop(0)
        rows = []
        step = 6 if lang in ("az", "en") else 5
        index = 0
        while index + step - 1 < len(data):
            chunk = data[index:index + step]
            if lang in ("az", "en") and re.fullmatch(r"\d+", chunk[0]):
                chunk = chunk[1:]
            rows.append(chunk[:5])
            index += step
        rows_by_lang[lang] = rows
    max_foreign = max((len(rows) for rows in rows_by_lang.values()), default=0)
    for i in range(max_foreign):
        az = rows_by_lang.get("az", [])
        en = rows_by_lang.get("en", [])
        ru = rows_by_lang.get("ru", [])
        az_row = az[i] if i < len(az) else []
        en_row = en[i] if i < len(en) else []
        ru_row = ru[i] if i < len(ru) else []
        country = first_not_empty(*(row[0] for row in (az_row, en_row, ru_row) if row))
        name = first_not_empty(*(row[1] for row in (az_row, en_row, ru_row) if len(row) > 1))
        if not country or not name:
            continue
        foreign.append({
            "sourceId": f"page31/xarici/{i}",
            "countryAz": short((az_row[0] if az_row else "") or country, 200),
            "countryEn": short((en_row[0] if en_row else "") or country, 200),
            "countryRu": short((ru_row[0] if ru_row else "") or country, 200),
            "fullNameAz": short((az_row[1] if len(az_row) > 1 else "") or name, 200),
            "fullNameEn": short((en_row[1] if len(en_row) > 1 else "") or name, 200),
            "fullNameRu": short((ru_row[1] if len(ru_row) > 1 else "") or name, 200),
            "dutyAz": short((az_row[2] if len(az_row) > 2 else "") or "-", 200),
            "dutyEn": short((en_row[2] if len(en_row) > 2 else "") or "-", 200),
            "dutyRu": short((ru_row[2] if len(ru_row) > 2 else "") or "-", 200),
            "companyAz": short((az_row[3] if len(az_row) > 3 else "") or "-", 200),
            "companyEn": short((en_row[3] if len(en_row) > 3 else "") or "-", 200),
            "companyRu": short((ru_row[3] if len(ru_row) > 3 else "") or "-", 200),
        })

    publications = []
    cover_ids = serialized_ids(page_meta[113].get("uz"))
    pdfs = split_lines(page_meta[113].get("jurnal"))
    for index, cover_id in enumerate(cover_ids):
        title = posts.get(cover_id, {}).get("title") or f"Biznes həyatı #{index + 1}"
        publications.append({
            "sourceId": f"page113/publication/{index}",
            "titleAz": short(title, 500),
            "titleEn": short(title, 500),
            "titleRu": short(title, 500),
            "titleImage": media(attachment_url(posts, cover_id), f"page113/publication/{index}/cover"),
            "pdf": media(pdfs[index] if index < len(pdfs) else "", f"page113/publication/{index}/pdf"),
        })

    partners = []
    logos = serialized_ids(page_meta[624].get("loqolar") or page_meta[624].get("logolar"))
    links = split_lines(page_meta[624].get("link") or page_meta[624].get("linkler"))
    for index, logo_id in enumerate(logos):
        site = links[index] if index < len(links) else "#"
        if site:
            partners.append({
                "sourceId": f"page624/partner/{index}",
                "site": site,
                "image": media(attachment_url(posts, logo_id), f"page624/partner/{index}/image"),
            })

    international = []
    intl_ids = serialized_ids(page_meta[49].get("beynelxalq"))
    intl_links = split_lines(page_meta[49].get("beynelxalqlinkler"))
    for index, logo_id in enumerate(intl_ids):
        link = intl_links[index] if index < len(intl_links) else "#"
        international.append({
            "sourceId": f"page49/international/{index}",
            "link": link,
            "icon": media(attachment_url(posts, logo_id), f"page49/international/{index}/icon"),
        })

    galleries = [
        {
            "sourceId": f"page624/gallery/{index}",
            "image": media(attachment_url(posts, image_id), f"page624/gallery/{index}/image"),
        }
        for index, image_id in enumerate(serialized_ids(page_meta[624].get("qalereya")))
        if attachment_url(posts, image_id)
    ]

    faqs = []
    for i in range(1, 15):
        question_az = clean(page_meta[1309].get(f"sual{i}"))
        question_en = clean(page_meta[1309].get(f"sual{i}eng"))
        question_ru = clean(page_meta[1309].get(f"sual{i}rus"))
        question = first_not_empty(question_az, question_en, question_ru)
        if not question:
            continue
        answer_az = first_not_empty(page_meta[1309].get(f"cavab{i}"), "Tezliklə")
        answer_en = first_not_empty(page_meta[1309].get(f"cavab{i}eng"), answer_az)
        answer_ru = first_not_empty(page_meta[1309].get(f"cavab{i}rus"), answer_az)
        faqs.append({
            "sourceId": f"page1309/faq/{i}",
            "questionAz": short(question_az or question, 1000),
            "questionEn": short(question_en or question, 1000),
            "questionRu": short(question_ru or question, 1000),
            "answerAz": answer_az,
            "answerEn": answer_en,
            "answerRu": answer_ru,
        })

    president_text = "\n\n".join([
        clean(posts.get(627, {}).get("content")),
        clean(posts.get(2747, {}).get("content")),
        clean(posts.get(3423, {}).get("content")),
    ]).strip()
    president = {
        "text": president_text,
        "image": media(first_not_empty(*image_sources(posts.get(627, {}).get("content")), FALLBACK_IMAGE), "page627/president/image"),
    }

    settings = [
        {"key": "KonfederasiyaHaqqinda", "value": clean(posts.get(626, {}).get("content"))},
        {"key": "Missiyamiz", "value": clean(posts.get(1570, {}).get("content"))},
        {"key": "Membership", "value": clean(posts.get(1051, {}).get("content"))},
        {"key": "Location", "value": "Bakı şəhəri, Azərbaycan"},
        {"key": "Number", "value": "+994 12 465 76 92"},
        {"key": "Email", "value": "office@ask.org.az"},
    ]

    services = []
    service_items = re.findall(r"<li[^>]*>(.*?)</li>", posts.get(1064, {}).get("content", ""), flags=re.S | re.I)
    service_items_en = re.findall(r"<li[^>]*>(.*?)</li>", posts.get(2778, {}).get("content", ""), flags=re.S | re.I)
    service_items_ru = re.findall(r"<li[^>]*>(.*?)</li>", posts.get(3446, {}).get("content", ""), flags=re.S | re.I)
    max_services = max(len(service_items), len(service_items_en), len(service_items_ru))
    for i in range(max_services):
        az = re.sub(r"<[^>]+>", "", service_items[i]).strip() if i < len(service_items) else ""
        en = re.sub(r"<[^>]+>", "", service_items_en[i]).strip() if i < len(service_items_en) else ""
        ru = re.sub(r"<[^>]+>", "", service_items_ru[i]).strip() if i < len(service_items_ru) else ""
        name = first_not_empty(az, en, ru)
        if name:
            services.append({
                "sourceId": f"page1064/service/{i}",
                "nameAz": short(az or name, 200),
                "nameEn": short(en or name, 200),
                "nameRu": short(ru or name, 200),
                "image": media(FALLBACK_IMAGE, f"page1064/service/{i}/image"),
            })

    useful_links = [
        {
            "sourceId": f"page624/useful-link/{i}",
            "titleAz": short(posts.get(logos[i], {}).get("title") or link, 300),
            "titleEn": short(posts.get(logos[i], {}).get("title") or link, 300),
            "titleRu": short(posts.get(logos[i], {}).get("title") or link, 300),
            "link": link,
        }
        for i, link in enumerate(links)
    ]

    return {
        "directors": directors,
        "management": management,
        "committees": committees,
        "districtRepresentatives": districts,
        "foreignRepresentatives": foreign,
        "publications": publications,
        "partners": partners,
        "internationalSolidarity": international,
        "galleries": galleries,
        "faqs": faqs,
        "president": president,
        "settings": settings,
        "services": services,
        "usefulLinks": useful_links,
    }


def main():
    source = Path(sys.argv[1] if len(sys.argv) > 1 else "backup.sql")
    output = Path(sys.argv[2] if len(sys.argv) > 2 else "src/App.API/SeedData/wordpress-seed.json")
    posts, meta, terms, taxonomies, relationships, translations, groups = load_backup(source)
    news, announcements = build_post_records(posts, meta, terms, taxonomies, relationships, groups)
    exhibitions, trainings, business_forums, press_release_news = build_event_records(posts, meta, terms, taxonomies, relationships, groups)
    page_records = build_page_records(posts, meta)

    seed = {
        "source": str(source),
        "generatedBy": "tools/extract_wordpress_seed.py",
        "counts": {},
        "news": news + press_release_news,
        "announcements": announcements,
        "exhibitions": exhibitions,
        "trainings": trainings,
        "businessForums": business_forums,
        **page_records,
    }
    seed["counts"] = {
        key: len(value)
        for key, value in seed.items()
        if isinstance(value, list)
    }

    output.parent.mkdir(parents=True, exist_ok=True)
    output.write_text(json.dumps(seed, ensure_ascii=False, indent=2), encoding="utf-8")
    print(json.dumps(seed["counts"], ensure_ascii=False, indent=2))


if __name__ == "__main__":
    main()
