# Frontend/Admin Changes For WordPress Backup Data

Bu backend update-dən sonra deploy zamanı `backup.sql`-dən çıxarılmış data `src/App.API/SeedData/wordpress-seed.json` vasitəsilə boş DB-yə seed olunur. Seed restart zamanı mövcud datanı overwrite etmir; admin paneldə edilən dəyişikliklər qorunur.

## News Contract Change

`News` modelinə multilingual title əlavə edildi.

Admin `POST /api/News` və `PUT /api/News/{id}` multipart form-data request-lərinə bu field-ləri əlavə etməlidir:

- `TitleAz`
- `TitleEn`
- `TitleRu`
- `NewsTextAz`
- `NewsTextEn`
- `NewsTextRu`
- `TitleImage`

Public/admin response artıq belə gəlir:

```json
{
  "id": "guid",
  "titleImageUrl": "string",
  "title": "current-language title",
  "newsText": "current-language html/text",
  "imageUrls": ["string"],
  "isDeactive": false,
  "createDate": "2025-01-01T00:00:00"
}
```

Frontend xəbər list/detail UI-larında headline üçün artıq `title` istifadə olunmalıdır. Əvvəl title kimi `newsText`-dən substring çıxarılırdısa, onu ləğv edin.

## Media URLs

WordPress silinəcəyi üçün seed media-ları köhnə domenlərdən asılı qalmamalıdır. Deploy-dan əvvəl backend repo-da bu command işlədilməlidir:

```bash
python3 tools/mirror_wordpress_media.py
```

Script `src/App.API/SeedData/wordpress-seed.json` içindəki bütün `wp-content/uploads` və `res.cloudinary.com/.../upload/...` linklərini tapır, faylları `src/App.API/SeedData/wordpress-media` altına yükləyir və JSON-u `seed-media/...` path-lərinə rewrite edir. Xəta verən media faylı ümumi prosesi dayandırmır; uğurlu fayllar yenə rewrite olunur. Endirilə bilməyən image URL-lər ASK logo fallback media-sına rewrite edilir; PDF/doc fayllar fail siyahısında qalır.

Deploy zamanı backend startup-da həmin mirror edilmiş faylları MinIO bucket-ə `wordpress-seed/...` key-ləri ilə upload edir. Backend API response zamanı URL-ləri öz domenində belə qaytarır:

```text
https://backend-domain/api/media/wordpress-seed/{path}
```

Frontend bu URL-ləri normal image/pdf URL kimi render etməlidir. `titleImageUrl`, `imageUrls`, `pdfUrl`, `imageUrl`, `iconUrl` dəyərlərinə əlavə `/api/media` prefix-i verməyin.

## HTML Content

WordPress mətnləri orijinal HTML kimi saxlanılıb. Backend HTML içindəki mirror edilmiş `seed-media/...` linklərini də `/api/media/wordpress-seed/...` URL-lərinə çevirir. Detail səhifələrində content render edən komponent HTML qəbul etməlidir:

- News: `newsText`
- Announcement: `text`
- Exhibition/Training/BusinessForum: `text`
- President/Settings text pages: `text` və ya setting `stringValue`

Admin editor HTML-i pozmadan edit edə bilməlidir. Plain textarea istifadə edilirsə belə HTML saxlanmalıdır; rich text editor varsa sanitize qaydaları `<p>`, `<strong>`, `<em>`, `<a>`, `<img>`, `<table>`, `<ul>`, `<li>` tag-lərini silməməlidir.

## Seeded Sections

Backend seed aşağıdakı dataları doldurur:

- `News`: 997 multilingual xəbər qrupu
- `Announcement`: 11 elan
- `Exhibition`: 105 sərgi
- `Training`: 145 təlim/seminar
- `BusinessForum`: 65 biznes forum
- `Director`: 9 direktor
- `Management`: 100 idarə heyəti üzvü
- `Committee`: 16 komissiya
- `DistrictRepresentatives`: 48 regional nümayəndə
- `ForeignRepresentatives`: 25 xarici nümayəndə
- `Publication`: 14 Biznes Həyatı nəşri
- `Partner`: 19 partner
- `InternationalSolidarity`: 9 beynəlxalq tərəfdaşlıq linki
- `Gallery`: 9 şəkil
- `FAQ`: 14 sual-cavab
- `Service`: 7 xidmət
- `UsefulLink`: 18 faydalı link
- `President` və əsas text `Settings`

Əgər frontend/admin bu bölmələrin hamısını göstərmirsə, uyğun menu/page-lər əlavə olunmalıdır. Backend-də CRUD endpoint-lər bu entity-lər üçün mövcuddur.

## Deploy Note

Seed yalnız boş cədvəllərə insert edir. Production DB-də köhnə test data varsa və backup datasının görünməsini istəyirsinizsə, həmin cədvəlləri təmiz DB ilə deploy edin və ya migration/import öncəsi manual backup alın. Mövcud data üzərinə avtomatik overwrite qəsdən edilməyib, çünki admin edit-ləri itə bilər.
