#!/usr/bin/env python3
import argparse
import hashlib
import json
import mimetypes
import re
import time
import urllib.error
import urllib.parse
import urllib.request
from pathlib import Path

MEDIA_URL_RE = re.compile(
    r"https?://(?:"
    r"[^\"'\s<>]+/wp-content/uploads/[^\"'\s<>]+"
    r"|res\.cloudinary\.com/[^\"'\s<>]+/(?:image|raw|video)/upload/[^\"'\s<>]+"
    r")",
    re.I,
)
FALLBACK_MEDIA_URL = "https://ask.org.az/wp-content/uploads/2025/08/ASK-logo-600x400.jpg"


def iter_strings(value):
    if isinstance(value, dict):
        for child in value.values():
            yield from iter_strings(child)
    elif isinstance(value, list):
        for child in value:
            yield from iter_strings(child)
    elif isinstance(value, str):
        yield value


def rewrite_strings(value, replacements):
    if isinstance(value, dict):
        return {key: rewrite_strings(child, replacements) for key, child in value.items()}
    if isinstance(value, list):
        return [rewrite_strings(child, replacements) for child in value]
    if isinstance(value, str):
        for old, new in replacements.items():
            value = value.replace(old, new)
        return value
    return value


def clean_url(url):
    return url.rstrip(".,);]")


def safe_filename(url, content_type):
    parsed = urllib.parse.urlparse(url)
    basename = Path(urllib.parse.unquote(parsed.path)).name or "media"
    basename = re.sub(r"[^A-Za-z0-9._-]+", "-", basename).strip(".-") or "media"
    if "." not in basename:
        ext = mimetypes.guess_extension(content_type.split(";")[0].strip()) if content_type else None
        basename += ext or ".bin"
    digest = hashlib.sha1(url.encode("utf-8")).hexdigest()
    return digest[:2], f"{digest}-{basename}"


def is_document_url(url):
    extension = Path(urllib.parse.urlparse(url).path).suffix.lower()
    return extension in {".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".zip", ".rar"}


def download(url, destination, timeout, retries):
    headers = {
        "User-Agent": "ASK-backend-media-mirror/1.0"
    }

    for attempt in range(1, retries + 1):
        request = urllib.request.Request(url, headers=headers)
        try:
            with urllib.request.urlopen(request, timeout=timeout) as response:
                destination.parent.mkdir(parents=True, exist_ok=True)
                destination.write_bytes(response.read())
                return response.headers.get("Content-Type", "")
        except (urllib.error.URLError, TimeoutError, OSError):
            if attempt == retries:
                raise
            time.sleep(min(attempt * 1.5, 5))

    return ""


def candidate_urls(url):
    parsed = urllib.parse.urlparse(url)
    candidates = [url]

    if parsed.scheme == "http":
        candidates.append(urllib.parse.urlunparse(parsed._replace(scheme="https")))

    if parsed.netloc.lower() == "ask.muradov.ml":
        candidates.append(urllib.parse.urlunparse(parsed._replace(scheme="https", netloc="ask.org.az")))
        candidates.append(urllib.parse.urlunparse(parsed._replace(scheme="http", netloc="ask.org.az")))

    seen = set()
    for candidate in candidates:
        if candidate not in seen:
            seen.add(candidate)
            yield candidate


def main():
    parser = argparse.ArgumentParser(description="Mirror WordPress/Cloudinary media referenced by wordpress-seed.json.")
    parser.add_argument("--seed", default="src/App.API/SeedData/wordpress-seed.json")
    parser.add_argument("--media-root", default="src/App.API/SeedData/wordpress-media")
    parser.add_argument("--timeout", type=int, default=30)
    parser.add_argument("--retries", type=int, default=3)
    parser.add_argument("--continue-on-error", action="store_true", default=True)
    parser.add_argument("--fail-fast", action="store_true")
    parser.add_argument("--fallback-url", default=FALLBACK_MEDIA_URL)
    parser.add_argument("--no-fallback-for-failures", action="store_true")
    parser.add_argument("--dry-run", action="store_true")
    args = parser.parse_args()

    seed_path = Path(args.seed)
    media_root = Path(args.media_root)
    data = json.loads(seed_path.read_text(encoding="utf-8"))

    urls = sorted({clean_url(match.group(0)) for text in iter_strings(data) for match in MEDIA_URL_RE.finditer(text)})
    if args.fallback_url:
        urls = sorted(set(urls) | {args.fallback_url})
    replacements = {}
    failures = []

    if args.dry_run:
        print(f"Discovered: {len(urls)}")
        for url in urls:
            print(url)
        return

    for index, url in enumerate(urls, start=1):
        digest = hashlib.sha1(url.encode("utf-8")).hexdigest()
        known = list(media_root.glob(f"**/{digest}-*"))
        if known:
            relative = known[0].relative_to(media_root).as_posix()
            replacements[url] = f"seed-media/{relative}"
            print(f"[{index}/{len(urls)}] cached {url}")
            continue

        try:
            # First download to a temporary digest name; then rename with content-type aware extension when needed.
            tmp_dir = media_root / digest[:2]
            tmp_path = tmp_dir / f"{digest}.download"
            last_error = None
            content_type = ""
            for candidate in candidate_urls(url):
                try:
                    content_type = download(candidate, tmp_path, args.timeout, args.retries)
                    break
                except Exception as exc:
                    last_error = exc
            else:
                raise last_error or RuntimeError("download failed")

            bucket, filename = safe_filename(url, content_type)
            final_path = media_root / bucket / filename
            final_path.parent.mkdir(parents=True, exist_ok=True)
            tmp_path.replace(final_path)
            replacements[url] = f"seed-media/{final_path.relative_to(media_root).as_posix()}"
            print(f"[{index}/{len(urls)}] mirrored {url}")
        except Exception as exc:
            failures.append((url, str(exc)))
            print(f"[{index}/{len(urls)}] failed {url}: {exc}")
            if args.fail_fast:
                break

    if failures and args.fail_fast:
        print("\nStopped because a media file failed to download.")
        print("Re-run without --fail-fast to rewrite only successfully downloaded files.")
        raise SystemExit(1)

    fallback_rewrites = 0
    fallback_replacement = replacements.get(args.fallback_url)
    if failures and fallback_replacement and not args.no_fallback_for_failures:
        for url, _ in failures:
            if url != args.fallback_url and not is_document_url(url):
                replacements.setdefault(url, fallback_replacement)
                fallback_rewrites += 1

    rewritten = rewrite_strings(data, replacements)
    seed_path.write_text(json.dumps(rewritten, ensure_ascii=False, indent=2), encoding="utf-8")

    print(f"\nDiscovered: {len(urls)}")
    print(f"Rewritten: {len(replacements)}")
    print(f"Fallback rewritten: {fallback_rewrites}")
    print(f"Failed: {len(failures)}")
    if failures:
        print("\nFailures:")
        for url, error in failures:
            print(f"- {url}: {error}")


if __name__ == "__main__":
    main()
