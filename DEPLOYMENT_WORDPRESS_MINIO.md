# WordPress Backup + MinIO Deploy

Bu backend artıq `backup.sql` faylını production repo artefaktı kimi tələb etmir. `backup.sql` yalnız lokalda `wordpress-seed.json` yaratmaq üçündür; deploy-a gedən data `src/App.API/SeedData/wordpress-seed.json` və `src/App.API/SeedData/wordpress-media` qovluğudur.

## 1. Backup SQL-dən seed JSON yarat

`backup.sql` dəyişibsə bunu lokalda bir dəfə işlət:

```bash
python3 tools/extract_wordpress_seed.py backup.sql src/App.API/SeedData/wordpress-seed.json
```

Əgər `backup.sql` dəyişməyibsə, bu addımı təkrar etməyə ehtiyac yoxdur.

## 2. WordPress media fayllarını mirror et

Bu command köhnə WordPress/Cloudinary URL-ləri əvəzinə lokal seed media hazırlayır:

```bash
python3 tools/mirror_wordpress_media.py
```

Script artıq fail-də dayanmayacaq. Hansısa şəkil 404/timeout versə, onu `Failed` siyahısına yazacaq, amma qalan şəkilləri endirib `wordpress-seed.json` içində uğurlu URL-ləri `seed-media/...` path-inə rewrite edəcək.

Image faylı endirilə bilməsə, script default olaraq onu ASK logo fallback faylına rewrite edir ki, WordPress bağlananda səhifələrdə qırıq image qalmasın. PDF/doc kimi fayllar fallback edilmir; onları `Failures` siyahısından ayrıca yoxlamaq lazımdır.

Fail-də dərhal dayanmaq istəsən:

```bash
python3 tools/mirror_wordpress_media.py --fail-fast
```

Fallback image davranışını söndürmək istəsən:

```bash
python3 tools/mirror_wordpress_media.py --no-fallback-for-failures
```

Qalan köhnə URL-ləri görmək üçün:

```bash
python3 tools/mirror_wordpress_media.py --dry-run
```

## 3. Deploy üçün fayllar

Deploy artefaktında bunlar olmalıdır:

- `src/App.API/SeedData/wordpress-seed.json`
- `src/App.API/SeedData/wordpress-media/**`
- `docker-compose.yml`
- `.env`

Deploy artefaktında bu lazım deyil:

- `backup.sql`

## 4. `.env` dəyərləri

`.env.example` MinIO-ya uyğun yeniləndi. Minimum dəyişməli dəyərlər:

```env
DB_SA_PASSWORD=CHANGE_THIS_SQL_PASSWORD_Use_20plus_Chars!
Jwt__Key=CHANGE_THIS_TO_A_LONG_RANDOM_64PLUS_CHARACTER_SECRET_KEY

MINIO_CONTAINER_NAME=ask.org.az-minio
MINIO_ROOT_USER=ask_minio
MINIO_ROOT_PASSWORD=CHANGE_THIS_MINIO_PASSWORD_Use_20plus_Chars!
MINIO_BUCKET_NAME=ask-media
MINIO_API_PORT=9000
MINIO_CONSOLE_PORT=9001
WORDPRESS_SEED_ENABLED=true
```

## 5. Deploy command

Sən işlədəcəksən:

```bash
docker compose up -d --build
```

API startup zamanı bunları edir:

- EF migration-ları tətbiq edir
- Fresh/boş cədvəllərə `wordpress-seed.json` datasını insert edir
- `wordpress-media` fayllarını MinIO bucket-ə `wordpress-seed/...` key-ləri ilə upload edir
- DB-də image/pdf dəyərlərini backend media route-u ilə işləyəcək object key kimi saxlayır

## 6. Yoxlama

Container-lərə bax:

```bash
docker compose ps
```

API loglarında seed və media upload nəticəsinə bax:

```bash
docker compose logs api --tail=200
```

MinIO console:

```text
http://SERVER_IP:9001
```

Login: `.env` içindəki `MINIO_ROOT_USER` və `MINIO_ROOT_PASSWORD`.

Bucket içində `ask-media/wordpress-seed/...` faylları görünməlidir.

Backend media URL nümunəsi:

```text
https://api-domain/api/media/wordpress-seed/{path}
```

## 7. Vacib davranış

Seed yalnız boş cədvəllərə yazır. Production DB-də köhnə/test data varsa, həmin cədvəldə seed skip olacaq. Backup datasını tam görmək üçün fresh DB volume ilə deploy etmək lazımdır.

Admin paneldən edit olunan datalar restart zamanı overwrite edilmir. Yeni upload edilən şəkillər də MinIO-ya düşür.
