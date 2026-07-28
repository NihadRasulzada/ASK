# ASK Backend Frontend Handoff

Bu sənəd ASK backend-ini istifadə edəcək iki frontend tərəfi üçündür:

- public web sayt: ziyarətçilərə görünən səhifələr, siyahılar, xəbərlər, nəşrlər, PDF-lər, şəkillər;
- admin panel: login, CRUD əməliyyatları, fayl upload-ları, aktiv/deaktiv idarəetməsi, settings idarəetməsi.

Məqsəd frontend developerin backend koduna girmədən API contract-ı düzgün başa düşməsidir.

## Qısa Xülasə

- Backend ASP.NET Core API-dir.
- Route formatı əsasən `api/[ControllerName]` şəklindədir.
- Production API base URL hazırda: `https://askapi.isaaholic.cyou`
- Public endpoint-lər əsasən auth istəmir.
- Admin create/update/delete endpoint-ləri `Authorization: Bearer <token>` istəyir.
- Lokalizasiya `?lang=az`, `?lang=en`, `?lang=ru` query parametri ilə seçilir.
- `lang` göndərilməyəndə backend default `az` qaytarır.
- Media faylları artıq Cloudinary-dən yox, backend-in `/api/Media/...` endpoint-indən gəlir.
- Frontend MinIO-ya birbaşa bağlanmamalıdır. API response-da gələn `imageUrl`, `titleImageUrl`, `pdfUrl`, `mediaUrl`, `iconUrl`, `detailImageUrl` nədirsə, onu istifadə edin.
- `backup.sql` repo contract-ı deyil. Backend deploy zamanı öz migration/seed flow-u ilə datanı və media path-ləri idarə edir.

## Frontend Environment

Public sayt və admin panel üçün minimum env:

```env
NEXT_PUBLIC_API_BASE_URL=https://askapi.isaaholic.cyou
```

React/Vite üçün ad fərqli ola bilər:

```env
VITE_API_BASE_URL=https://askapi.isaaholic.cyou
```

Vacib: frontenddə Cloudinary cloud name, MinIO access key, MinIO secret key, bucket adı və ya storage endpoint saxlamayın. Bunlar backend internal config-dir.

## Ümumi Request Qaydaları

### Lokalizasiya

Lokalizə olunan response sahələri backend tərəfindən seçilir. Frontend üç dildə ayrıca field gözləməməlidir.

```http
GET /api/News?lang=az
GET /api/News?lang=en
GET /api/News?lang=ru
```

Response-da məsələn `title`, `text`, `name`, `question`, `answer` artıq seçilmiş dildə gəlir.

### Response Forması

Adi success:

```json
{
  "success": true,
  "message": "Resource retrieved successfully",
  "data": {}
}
```

Siyahı:

```json
{
  "success": true,
  "message": "Resources retrieved successfully",
  "data": []
}
```

Paginated siyahı:

```json
{
  "success": true,
  "message": "Publications retrieved successfully",
  "data": [],
  "paginationMetadata": {
    "pageIndex": 1,
    "pageSize": 10,
    "totalCount": 14,
    "totalPages": 2,
    "hasPreviousPage": false,
    "hasNextPage": true
  }
}
```

Validation xətası adətən `422` statusla gəlir:

```json
{
  "success": false,
  "message": "Validation failed",
  "errors": []
}
```

### Media URL Qaydası

Backend response-da media üçün tam URL qaytarır:

```json
{
  "titleImageUrl": "https://askapi.isaaholic.cyou/api/media/wordpress-seed/xx/file.jpg",
  "pdfUrl": "https://askapi.isaaholic.cyou/api/media/wordpress-seed/yy/file.pdf"
}
```

Frontend:

- image üçün: `<img src={item.titleImageUrl} />`
- PDF üçün: `<a href={item.pdfUrl} target="_blank" rel="noreferrer">PDF</a>`
- preview üçün: `<iframe src={item.pdfUrl} />` və ya browser-in native PDF viewer-i

Etməyin:

- Cloudinary URL qurmayın.
- MinIO public URL qurmayın.
- Media object key-i parse edib öz URL-inizi düzəltməyin.
- URL-lərdə `wordpress-seed`, `images/yyyy/MM`, `documents/yyyy/MM` kimi path-lərə logic bağlamayın. Bunlar storage detallarıdır.

Əgər media endpoint `404` verirsə, frontend fallback image göstərə bilər, amma normal halda bu backend/deploy/seed problemidir.

## Auth Flow

Admin panel login üçün:

```http
POST /api/Auth/login
Content-Type: application/json
```

Body:

```json
{
  "username": "admin_username",
  "password": "admin_password"
}
```

Response:

```json
{
  "success": true,
  "data": {
    "token": "jwt_access_token",
    "refreshToken": "refresh_token",
    "tokenExpiresAt": "2026-07-28T12:00:00Z"
  }
}
```

Admin request-lərdə:

```http
Authorization: Bearer <token>
```

Refresh:

```http
POST /api/Auth/refresh
Content-Type: application/json
```

```json
{
  "username": "admin_username",
  "refreshToken": "refresh_token"
}
```

Change password:

```http
POST /api/Auth/change-password
Authorization: Bearer <token>
Content-Type: application/json
```

```json
{
  "currentPassword": "old_password",
  "newPassword": "new_password",
  "confirmPassword": "new_password"
}
```

## File Upload Qaydaları

Fayl olan create/update endpoint-ləri `multipart/form-data` qəbul edir.

Fetch istifadə edirsinizsə, `Content-Type` header-i əl ilə yazmayın. Browser özü boundary əlavə etməlidir.

```ts
const form = new FormData();
form.append("TitleAz", values.titleAz);
form.append("TitleEn", values.titleEn);
form.append("TitleRu", values.titleRu);
form.append("TitleImage", titleImageFile);
form.append("PdfFile", pdfFile);

await fetch(`${API}/api/Publication?lang=az`, {
  method: "POST",
  headers: {
    Authorization: `Bearer ${token}`
  },
  body: form
});
```

Update zamanı optional fayl göndərilməsə, backend mövcud faylı saxlayır. Buna görə admin edit formunda istifadəçi yeni fayl seçməyibsə həmin field-i FormData-ya əlavə etməyin.

Maksimum request body backenddə 12 MB-dır. Böyük şəkilləri frontenddə sıxmaq yaxşıdır.

## Public Sayt Üçün Səhifə Xəritəsi

### Ana Səhifə

Tövsiyə olunan data:

- `GET /api/Setting/HeroTitle?lang=az`
- `GET /api/Setting/HeroDescription?lang=az`
- `GET /api/Setting/HeroStatMemberCount?lang=az`
- `GET /api/Setting/HeroStatPartnerCount?lang=az`
- `GET /api/Setting/HeroStatEventCount?lang=az`
- `GET /api/Service?lang=az`
- `GET /api/News?lang=az`
- `GET /api/Announcement?lang=az`
- `GET /api/Partner`
- `GET /api/UsefulLink?lang=az`
- `GET /api/Currency`

### Haqqımızda

Tövsiyə olunan data:

- `GET /api/Setting/KonfederasiyaHaqqinda?lang=az`
- `GET /api/Setting/Missiyamiz?lang=az`
- `GET /api/Setting/Membership?lang=az`
- `GET /api/President?lang=az`
- `GET /api/Presidium?lang=az`
- `GET /api/Management?lang=az`
- `GET /api/Committee?lang=az`
- `GET /api/Director?lang=az`
- `GET /api/OurValues?lang=az`

### Rəhbərlik və Nümayəndəliklər

Tövsiyə olunan data:

- `GET /api/Director?lang=az`
- `GET /api/Director/{id}?lang=az`
- `GET /api/Management?lang=az`
- `GET /api/DistrictRepresentatives?lang=az`
- `GET /api/ForeignRepresentatives?lang=az`

### Xəbərlər

Siyahı:

```http
GET /api/News?lang=az
```

Detail:

```http
GET /api/News/{id}?lang=az
```

Response field-ləri:

- `id`
- `titleImageUrl`
- `title`
- `newsText`
- `imageUrls`
- `isDeactive`
- `createDate`

`newsText` HTML ola bilər. Render edərkən XSS təhlükəsizliyi üçün frontend tərəfdə sanitization edin. Backend WordPress seed-dən gələn media path-ləri API media URL-lərinə çevirir.

### Elanlar

```http
GET /api/Announcement?lang=az
GET /api/Announcement/{id}?lang=az
```

Field-lər:

- `id`
- `title`
- `titleImageUrl`
- `text`
- `created`

### Tədbirlər, Təlimlər, Sərgilər, Biznes Forum

Exhibition:

```http
GET /api/Exhibition?pageIndex=1&pageSize=10&lang=az
GET /api/Exhibition/{id}?lang=az
GET /api/Exhibition/all?lang=az
```

Training:

```http
GET /api/Training?pageIndex=1&pageSize=10&lang=az
GET /api/Training/{id}?lang=az
GET /api/Training/all?lang=az
```

Business Forum:

```http
GET /api/BusinessForum?pageIndex=1&pageSize=10&lang=az
GET /api/BusinessForum/{id}?lang=az
```

Calendar:

```http
GET /api/Calendar?date=2026-07-28&lang=az
```

Calendar response:

- `selectedDate`
- `dayEvents`
- `monthEventDates`

### Nəşrlər

Siyahı:

```http
GET /api/Publication?pageIndex=1&pageSize=10&lang=az
```

Detail:

```http
GET /api/Publication/{id}?lang=az
```

Response:

```json
{
  "id": "4d4a62d5-dfbc-4d4f-9917-2fdb6aa9c418",
  "title": "Noyabr 2021 (No28)",
  "titleImageUrl": "https://askapi.isaaholic.cyou/api/media/...",
  "pdfUrl": "https://askapi.isaaholic.cyou/api/media/..."
}
```

Bu endpoint test üçün ən yaxşı nümunədir, çünki həm şəkil, həm PDF qaytarır.

### Faydalı Sənədlər

Bu sənədlər `Setting` kimi saxlanır. Link tipli setting-lər `mediaUrl` qaytarır:

- `BasKollektivSazis`
- `AzRespublikasininKonstitutsiyasi`
- `QeyriHokumetteshkilatlariHaqqindaQanun`
- `AzRespublikasiEmekMecellesi`
- `AzRespublikasiVergiMecellesi`
- `AzRespublikasiMulkiMecellesi`
- `KomissiyaHaqqinda`
- `KomissiyaninEsasnamesi`
- `Nizamname`

Nümunə:

```http
GET /api/Setting/Nizamname?lang=az
```

Response:

```json
{
  "success": true,
  "data": {
    "id": "11111111-0010-0000-0000-000000000000",
    "key": "Nizamname",
    "stringValue": null,
    "mediaUrl": "https://askapi.isaaholic.cyou/api/media/...",
    "valueType": 0
  }
}
```

`valueType`: `0 = Link`, `1 = Text`.

### Əlaqə

Settings:

- `GET /api/Setting/Location?lang=az`
- `GET /api/Setting/Number?lang=az`
- `GET /api/Setting/Email?lang=az`

Social/solidarity links:

```http
GET /api/InternationalSolidarity
```

### FAQ

Public list:

```http
GET /api/FAQ?lang=az
GET /api/FAQ/{id}?lang=az
```

Admin list with inactive items:

```http
GET /api/FAQ/all?lang=az
```

Qeyd: `POST /api/FAQ/inquiry` hazırda auth istəyir. Public FAQ sual göndərmə formu qurulacaqsa, backenddə ayrıca public endpoint açılmalıdır və ya mövcud endpointdən `[Authorize]` çıxarılmalıdır.

## Public Endpoint Reference

| Bölmə | Endpoint | Qeyd |
| --- | --- | --- |
| Services | `GET /api/Service?lang=az` | Aktiv xidmətlər |
| News | `GET /api/News?lang=az` | Aktiv xəbərlər |
| News detail | `GET /api/News/{id}?lang=az` | `imageUrls` daxil gəlir |
| Announcements | `GET /api/Announcement?lang=az` | Elanlar |
| Publications | `GET /api/Publication?pageIndex=1&pageSize=10&lang=az` | Şəkil + PDF |
| Publication detail | `GET /api/Publication/{id}?lang=az` | Şəkil + PDF |
| Exhibition | `GET /api/Exhibition?pageIndex=1&pageSize=10&lang=az` | Paginated |
| Training | `GET /api/Training?pageIndex=1&pageSize=10&lang=az` | Paginated |
| Business Forum | `GET /api/BusinessForum?pageIndex=1&pageSize=10&lang=az` | Paginated |
| Calendar | `GET /api/Calendar?date=2026-07-28&lang=az` | Gün və ay eventləri |
| Director | `GET /api/Director?lang=az` | Aktiv direktorlar |
| Presidium | `GET /api/Presidium?lang=az` | Rəyasət heyəti |
| President | `GET /api/President?lang=az` | Prezident bloku |
| Management | `GET /api/Management?lang=az` | İdarə heyəti |
| Committee | `GET /api/Committee?lang=az` | Komitələr |
| District reps | `GET /api/DistrictRepresentatives?lang=az` | Rayon nümayəndələri |
| Foreign reps | `GET /api/ForeignRepresentatives?lang=az` | Xarici nümayəndələr |
| Our values | `GET /api/OurValues?lang=az` | Dəyərlər |
| Gallery | `GET /api/Gallery` | Şəkil qalereyası |
| Partners | `GET /api/Partner` | Logo + site |
| Videos | `GET /api/Video` | Video linkləri |
| Useful links | `GET /api/UsefulLink?lang=az` | Aktiv linklər |
| International solidarity | `GET /api/InternationalSolidarity` | Link + icon |
| FAQ | `GET /api/FAQ?lang=az` | Aktiv suallar |
| Currency | `GET /api/Currency` | Valyuta məzənnələri |
| Settings by key | `GET /api/Setting/{key}?lang=az` | Public |
| Media | `GET /api/Media/{path}` | API response URL-lərindən gəlir |

## Admin Endpoint Reference

### Auth

| İş | Endpoint | Body |
| --- | --- | --- |
| Login | `POST /api/Auth/login` | JSON: `username`, `password` |
| Refresh | `POST /api/Auth/refresh` | JSON: `username`, `refreshToken` |
| Change password | `POST /api/Auth/change-password` | JSON: `currentPassword`, `newPassword`, `confirmPassword` |

### CRUD

Bu endpoint-lərdə create/update/delete üçün `Authorization: Bearer <token>` lazımdır.

| Resurs | List | Create | Update | Delete | Aktiv/Deaktiv |
| --- | --- | --- | --- | --- | --- |
| Service | `GET /api/Service/including-deleted` | `POST /api/Service` multipart | `PUT /api/Service?id={id}` multipart | `DELETE /api/Service/{id}` | `PATCH /api/Service/{id}/activate`, `PATCH /api/Service/{id}/deactivate` |
| News | `GET /api/News/including-deleted` | `POST /api/News` multipart | `PUT /api/News/{id}` multipart | `DELETE /api/News/{id}` | `PATCH /api/News/{id}/activate`, `PATCH /api/News/{id}/deactivate` |
| News images | `GET /api/NewsImage` | `POST /api/NewsImage` multipart | `PUT /api/NewsImage?id={id}` multipart | `DELETE /api/NewsImage/{id}` | Yoxdur |
| Announcement | `GET /api/Announcement` | `POST /api/Announcement` multipart | `PUT /api/Announcement/{id}` multipart | `DELETE /api/Announcement/{id}` | Yoxdur |
| Publication | `GET /api/Publication` | `POST /api/Publication` multipart | `PUT /api/Publication/{id}` multipart | `DELETE /api/Publication/{id}` | Yoxdur |
| Exhibition | `GET /api/Exhibition/alldatedata` | `POST /api/Exhibition` multipart | `PUT /api/Exhibition/{id}` multipart | `DELETE /api/Exhibition/{id}` | `PATCH /api/Exhibition/activate/{id}`, `PATCH /api/Exhibition/deactivate/{id}` |
| Training | `GET /api/Training/alldatedata` | `POST /api/Training` multipart | `PUT /api/Training/{id}` multipart | `DELETE /api/Training/{id}` | `PATCH /api/Training/activate/{id}`, `PATCH /api/Training/deactivate/{id}` |
| Business Forum | `GET /api/BusinessForum` | `POST /api/BusinessForum` multipart | `PUT /api/BusinessForum/{id}` multipart | `DELETE /api/BusinessForum/{id}` | Yoxdur |
| Director | `GET /api/Director/all` | `POST /api/Director` multipart | `PUT /api/Director/{id}` multipart | `DELETE /api/Director/{id}` | `PATCH /api/Director/activate/{id}`, `PATCH /api/Director/deactivate/{id}` |
| Presidium | `GET /api/Presidium` | `POST /api/Presidium` multipart | `PUT /api/Presidium/{id}` multipart | `DELETE /api/Presidium/{id}` | Yoxdur |
| President | `GET /api/President` | `POST /api/President` multipart | `PUT /api/President/{id}` multipart | `DELETE /api/President/{id}` | Yoxdur |
| Our Values | `GET /api/OurValues` | `POST /api/OurValues` multipart | `PUT /api/OurValues/{id}` multipart | `DELETE /api/OurValues/{id}` | Yoxdur |
| Gallery | `GET /api/Gallery` | `POST /api/Gallery` multipart | `PUT /api/Gallery/{id}` multipart | `DELETE /api/Gallery/{id}` | Yoxdur |
| Partner | `GET /api/Partner` | `POST /api/Partner` multipart | `PUT /api/Partner/{id}` multipart | `DELETE /api/Partner/{id}` | Yoxdur |
| International Solidarity | `GET /api/InternationalSolidarity` | `POST /api/InternationalSolidarity` multipart | `PUT /api/InternationalSolidarity/{id}` multipart | `DELETE /api/InternationalSolidarity/{id}` | Yoxdur |
| Video | `GET /api/Video` | `POST /api/Video` JSON | `PUT /api/Video/{id}` JSON | `DELETE /api/Video/{id}` | Yoxdur |
| Useful Link | `GET /api/UsefulLink/all` | `POST /api/UsefulLink` JSON | `PUT /api/UsefulLink/{id}` JSON | `DELETE /api/UsefulLink/{id}` | `PATCH /api/UsefulLink/activate/{id}`, `PATCH /api/UsefulLink/deactivate/{id}` |
| FAQ | `GET /api/FAQ/all` | `POST /api/FAQ` JSON | `PUT /api/FAQ/{id}` JSON | `DELETE /api/FAQ/{id}` | `PATCH /api/FAQ/activate/{id}`, `PATCH /api/FAQ/deactivate/{id}` |
| Management | `GET /api/Management` | `POST /api/Management` JSON | `PUT /api/Management/{id}` JSON | `DELETE /api/Management/{id}` | Yoxdur |
| Committee | `GET /api/Committee` | `POST /api/Committee` JSON | `PUT /api/Committee/{id}` JSON | `DELETE /api/Committee/{id}` | Yoxdur |
| District Representatives | `GET /api/DistrictRepresentatives` | `POST /api/DistrictRepresentatives` JSON | `PUT /api/DistrictRepresentatives/{id}` JSON | `DELETE /api/DistrictRepresentatives/{id}` | Yoxdur |
| Foreign Representatives | `GET /api/ForeignRepresentatives` | `POST /api/ForeignRepresentatives` JSON | `PUT /api/ForeignRepresentatives/{id}` JSON | `DELETE /api/ForeignRepresentatives/{id}` | Yoxdur |
| Settings | `GET /api/Setting` | Yoxdur | `PUT /api/Setting/{key}` multipart | Yoxdur | Yoxdur |
| Currency period | `GET /api/Currency/change` | Query istifadəçi period setting-i üçün | Yoxdur | Yoxdur | Yoxdur |

Qeyd: `Service` və `NewsImage` update route-ları legacy formadadır: `PUT /api/Service?id={id}` və `PUT /api/NewsImage?id={id}`. Bu ikisində `id` route path-də yox, query string-də getməlidir.

## Admin Form Field-ləri

### Multipart Resurslar

`Service`

- create: `Image`, `NameAz`, `NameEn`, `NameRu`
- update: `Image?`, `NameAz`, `NameEn`, `NameRu`
- response: `id`, `imageUrl`, `name`, `isDeactive`

`News`

- create: `TitleImage`, `TitleAz`, `TitleEn`, `TitleRu`, `NewsTextAz`, `NewsTextEn`, `NewsTextRu`
- update: `TitleImage?`, `TitleAz`, `TitleEn`, `TitleRu`, `NewsTextAz`, `NewsTextEn`, `NewsTextRu`
- response: `id`, `titleImageUrl`, `title`, `newsText`, `imageUrls`, `isDeactive`, `createDate`

`NewsImage`

- create: `NewsId`, `Image`
- update: `Id`, `Image`
- response: `id`, `imageUrl`, `newsId`

`Announcement`

- create: `TitleAz`, `TitleEn`, `TitleRu`, `TitleImage`, `TextAz`, `TextEn`, `TextRu`
- update: `TitleAz`, `TitleEn`, `TitleRu`, `TitleImage?`, `TextAz`, `TextEn`, `TextRu`
- response: `id`, `title`, `titleImageUrl`, `text`, `created`

`Publication`

- create: `TitleAz`, `TitleEn`, `TitleRu`, `TitleImage`, `PdfFile`
- update: `TitleAz`, `TitleEn`, `TitleRu`, `TitleImage?`, `PdfFile?`
- response: `id`, `title`, `titleImageUrl`, `pdfUrl`

`Exhibition`

- create: `TitleAz`, `TitleEn`, `TitleRu`, `TextAz`, `TextEn`, `TextRu`, `Image`, `StartDate`, `EndDate`
- update: `TitleAz`, `TitleEn`, `TitleRu`, `TextAz`, `TextEn`, `TextRu`, `Image?`, `StartDate`, `EndDate`
- response: `id`, `title`, `text`, `titleImageUrl`, `isDeactive`, `created`

`Training`

- create: `TitleAz`, `TitleEn`, `TitleRu`, `TextAz`, `TextEn`, `TextRu`, `Image`, `StartDate`, `EndDate`
- update: `TitleAz`, `TitleEn`, `TitleRu`, `TextAz`, `TextEn`, `TextRu`, `Image?`, `StartDate`, `EndDate`
- response: `id`, `title`, `text`, `titleImageUrl`, `isDeactive`, `created`

`BusinessForum`

- create: `TitleAz`, `TitleEn`, `TitleRu`, `TextAz`, `TextEn`, `TextRu`, `TitleImage`, `DetailImage`, `StartDate`, `EndDate`
- update: `TitleAz`, `TitleEn`, `TitleRu`, `TextAz`, `TextEn`, `TextRu`, `TitleImage?`, `DetailImage?`, `StartDate`, `EndDate`
- response: `id`, `title`, `text`, `titleImageUrl`, `detailImageUrl`, `createDate`

`Director`

- create: `Image`, `FullNameAz`, `FullNameEn`, `FullNameRu`, `DutyAz`, `DutyEn`, `DutyRu`, `DepartmentAz?`, `DepartmentEn?`, `DepartmentRu?`, `PhoneNumber?`, `Email?`
- update: `Image?`, same text fields
- response: `id`, `imageUrl`, `fullName`, `duty`, `department`, `phoneNumber`, `email`, `isDeactive`

`Presidium`

- create: `FullNameAz`, `FullNameEn`, `FullNameRu`, `PositionAz`, `PositionEn`, `PositionRu`, `Image`
- update: same fields, `Image?`
- response: `id`, `fullName`, `position`, `imageUrl`

`President`

- create: `Image`, `Text`
- update: `Image?`, `Text`
- response: `id`, `imageUrl`, `text`

`OurValues`

- create: `TitleAz`, `TitleEn`, `TitleRu`, `Image`
- update: `TitleAz`, `TitleEn`, `TitleRu`, `Image?`
- response: `id`, `title`, `imageUrl`

`Gallery`

- create: `Image`
- update: `Image?`
- response: `id`, `imageUrl`

`Partner`

- create: `Image`, `Site`
- update: `Image?`, `Site`
- response: `id`, `imageUrl`, `site`

`InternationalSolidarity`

- create: `Link`, `Icon`
- update: `Link`, `Icon?`
- response: `id`, `link`, `iconUrl`

### JSON Resurslar

JSON request-lərdə camelCase istifadə edin. ASP.NET model binding case-insensitive olsa da, frontend convention camelCase olmalıdır.

`Video`

```json
{
  "link": "https://youtube.com/...",
  "title": "Video title"
}
```

Response: `id`, `link`, `title`.

`UsefulLink`

```json
{
  "titleAz": "Azərbaycan dili başlıq",
  "titleEn": "English title",
  "titleRu": "Русский заголовок",
  "link": "https://example.com"
}
```

Response: `id`, `title`, `link`, `isDeactive`.

`FAQ`

```json
{
  "questionAz": "Sual",
  "questionEn": "Question",
  "questionRu": "Вопрос",
  "answerAz": "Cavab",
  "answerEn": "Answer",
  "answerRu": "Ответ"
}
```

Response: `id`, `question`, `answer`, `isDeactive`.

`Management`

```json
{
  "fullNameAz": "Ad Soyad",
  "fullNameEn": "Full Name",
  "fullNameRu": "Имя Фамилия",
  "companyAz": "Şirkət",
  "companyEn": "Company",
  "companyRu": "Компания"
}
```

Response: `id`, `fullName`, `company`.

`Committee`

```json
{
  "nameAz": "Komitə adı",
  "nameEn": "Committee name",
  "nameRu": "Название комитета",
  "chairmanAz": "Sədr",
  "chairmanEn": "Chairman",
  "chairmanRu": "Председатель",
  "vicePresidentAz": "Vitse-prezident",
  "vicePresidentEn": "Vice president",
  "vicePresidentRu": "Вице-президент"
}
```

Response: `id`, `name`, `chairman`, `vicePresident`.

`DistrictRepresentatives`

```json
{
  "districtAz": "Rayon",
  "districtEn": "District",
  "districtRu": "Район",
  "fullNameAz": "Ad Soyad",
  "fullNameEn": "Full Name",
  "fullNameRu": "Имя Фамилия",
  "companyAz": "Şirkət",
  "companyEn": "Company",
  "companyRu": "Компания"
}
```

Response: `id`, `district`, `fullName`, `company`.

`ForeignRepresentatives`

```json
{
  "countryAz": "Ölkə",
  "countryEn": "Country",
  "countryRu": "Страна",
  "fullNameAz": "Ad Soyad",
  "fullNameEn": "Full Name",
  "fullNameRu": "Имя Фамилия",
  "companyAz": "Şirkət",
  "companyEn": "Company",
  "companyRu": "Компания",
  "dutyAz": "Vəzifə",
  "dutyEn": "Duty",
  "dutyRu": "Должность"
}
```

Response: `id`, `country`, `fullName`, `company`, `duty`.

## Settings Admin

Settings iki tipdir:

- `Link = 0`: fayl/PDF saxlayır, response-da `mediaUrl` gəlir.
- `Text = 1`: mətn saxlayır, response-da `stringValue` gəlir.

Admin bütün settings-ləri görə bilər:

```http
GET /api/Setting
Authorization: Bearer <token>
```

Tək setting public oxuna bilər:

```http
GET /api/Setting/HeroTitle?lang=az
```

Text update:

```ts
const form = new FormData();
form.append("Value", "Yeni mətn");

await fetch(`${API}/api/Setting/HeroTitle`, {
  method: "PUT",
  headers: { Authorization: `Bearer ${token}` },
  body: form
});
```

Link/PDF update:

```ts
const form = new FormData();
form.append("File", pdfFile);

await fetch(`${API}/api/Setting/Nizamname`, {
  method: "PUT",
  headers: { Authorization: `Bearer ${token}` },
  body: form
});
```

Setting key-ləri:

| Key | Tip | Public istifadə |
| --- | --- | --- |
| `BasKollektivSazis` | Link | PDF/sənəd |
| `AzRespublikasininKonstitutsiyasi` | Link | PDF/sənəd |
| `QeyriHokumetteshkilatlariHaqqindaQanun` | Link | PDF/sənəd |
| `AzRespublikasiEmekMecellesi` | Link | PDF/sənəd |
| `AzRespublikasiVergiMecellesi` | Link | PDF/sənəd |
| `AzRespublikasiMulkiMecellesi` | Link | PDF/sənəd |
| `KomissiyaHaqqinda` | Link | PDF/sənəd |
| `KomissiyaninEsasnamesi` | Link | PDF/sənəd |
| `Nizamname` | Link | PDF/sənəd |
| `KonfederasiyaHaqqinda` | Text | About content |
| `Missiyamiz` | Text | Mission |
| `Membership` | Text | Membership content |
| `HeroTitle` | Text | Home hero title |
| `HeroDescription` | Text | Home hero description |
| `HeroStatMemberCount` | Text | Home statistic |
| `HeroStatPartnerCount` | Text | Home statistic |
| `HeroStatEventCount` | Text | Home statistic |
| `Location` | Text | Contact |
| `Number` | Text | Contact |
| `Email` | Text | Contact |

Admin paneldə settings edit edərkən `mediaUrl` göstərilməlidir. `cloudinaryUrl` artıq yoxdur.

## Kod Nümunələri

### API helper

```ts
const API = process.env.NEXT_PUBLIC_API_BASE_URL;

export async function apiGet<T>(path: string, lang = "az"): Promise<T> {
  const separator = path.includes("?") ? "&" : "?";
  const res = await fetch(`${API}${path}${separator}lang=${lang}`, {
    cache: "no-store"
  });

  if (!res.ok) {
    throw new Error(`API request failed: ${res.status}`);
  }

  const json = await res.json();
  return json.data as T;
}
```

### Publication list və PDF açmaq

```ts
type Publication = {
  id: string;
  title: string;
  titleImageUrl: string;
  pdfUrl: string;
};

const res = await fetch(
  `${API}/api/Publication?pageIndex=1&pageSize=10&lang=az`
);
const json = await res.json();
const publications = json.data as Publication[];
```

```tsx
{publications.map((item) => (
  <article key={item.id}>
    <img src={item.titleImageUrl} alt={item.title} />
    <h3>{item.title}</h3>
    <a href={item.pdfUrl} target="_blank" rel="noreferrer">
      PDF aç
    </a>
  </article>
))}
```

### Admin create publication

```ts
async function createPublication(values: {
  titleAz: string;
  titleEn: string;
  titleRu: string;
  titleImage: File;
  pdfFile: File;
}, token: string) {
  const form = new FormData();
  form.append("TitleAz", values.titleAz);
  form.append("TitleEn", values.titleEn);
  form.append("TitleRu", values.titleRu);
  form.append("TitleImage", values.titleImage);
  form.append("PdfFile", values.pdfFile);

  const res = await fetch(`${API}/api/Publication`, {
    method: "POST",
    headers: { Authorization: `Bearer ${token}` },
    body: form
  });

  if (!res.ok) {
    throw new Error(`Create failed: ${res.status}`);
  }

  return res.json();
}
```

### Admin update publication

```ts
async function updatePublication(id: string, values: {
  titleAz: string;
  titleEn: string;
  titleRu: string;
  titleImage?: File;
  pdfFile?: File;
}, token: string) {
  const form = new FormData();
  form.append("TitleAz", values.titleAz);
  form.append("TitleEn", values.titleEn);
  form.append("TitleRu", values.titleRu);

  if (values.titleImage) form.append("TitleImage", values.titleImage);
  if (values.pdfFile) form.append("PdfFile", values.pdfFile);

  const res = await fetch(`${API}/api/Publication/${id}`, {
    method: "PUT",
    headers: { Authorization: `Bearer ${token}` },
    body: form
  });

  if (!res.ok) {
    throw new Error(`Update failed: ${res.status}`);
  }

  return res.json();
}
```

### Admin activate/deactivate

```ts
await fetch(`${API}/api/News/${id}/deactivate`, {
  method: "PATCH",
  headers: { Authorization: `Bearer ${token}` }
});

await fetch(`${API}/api/News/${id}/activate`, {
  method: "PATCH",
  headers: { Authorization: `Bearer ${token}` }
});
```

## Frontenddə Diqqət Ediləcək Risklər

1. `lang` həmişə URL-ə əlavə edilsin. Əks halda default `az` gələcək.
2. Response field-ləri artıq lokalizə olunmuş gəlir. Public UI-da `titleAz/titleEn/titleRu` gözləməyin.
3. Admin create/update üçün üç dil field-ləri göndərilməlidir.
4. Fayl upload zamanı `Content-Type: multipart/form-data` əl ilə yazılmasın.
5. Update-də yeni fayl seçilməyibsə fayl field-i göndərilməsin.
6. PDF və şəkil URL-ləri backend response-dan olduğu kimi istifadə edilsin.
7. `mediaUrl` yeni settings contract-ıdır. `cloudinaryUrl` istifadə etməyin.
8. `newsText`, `text`, `answer`, settings text content HTML ola bilər. Render edərkən sanitization edin.
9. `GET /api/FAQ/inquiry` və `POST /api/FAQ/inquiry` auth istəyir. Public contact form üçün backend dəyişikliyi lazım ola bilər.
10. Some list endpoint-lər publicdə yalnız aktiv datanı qaytarır, admin üçün `all`, `including-deleted`, `alldatedata` route-ları istifadə edilməlidir.
11. `Service` və `NewsImage` update `id`-ni query-dən alır: `?id=...`.
12. Swagger yalnız development environment-də aktiv ola bilər. Production-da Swagger görünməyə bilər.

## Deploy və Seed Konteksti

Backend deploy zamanı:

- migration-lar apply olunur;
- WordPress-dən köçürülmüş seed JSON data DB-yə yazılır;
- seed media obyektləri MinIO-da `wordpress-seed/...` path-ləri altında saxlanır;
- yeni admin upload-ları API vasitəsilə MinIO-ya yazılır;
- API media URL-ləri `/api/Media/...` üzərindən servis edilir.

Frontend developer üçün nəticə: deploydan sonra `GET /api/Publication?...` kimi public endpoint-lərdə gələn `titleImageUrl` və `pdfUrl` açılmalıdır. `backup.sql` lokal/deploy mexanizminin müvəqqəti köməkçi faylıdır, frontend ona heç cür bağlı deyil.

## Tez Test Endpoint-ləri

Şəkil + PDF olan test:

```http
GET https://askapi.isaaholic.cyou/api/Publication?pageIndex=1&pageSize=1&lang=az
```

Response-da gələn:

- `data[0].titleImageUrl` image kimi açılmalıdır;
- `data[0].pdfUrl` PDF kimi açılmalıdır.

Xəbər image gallery test:

```http
GET https://askapi.isaaholic.cyou/api/News?lang=az
```

Detail:

```http
GET https://askapi.isaaholic.cyou/api/News/{id}?lang=az
```

Settings PDF test:

```http
GET https://askapi.isaaholic.cyou/api/Setting/Nizamname?lang=az
```

## Frontend Komandası Üçün Minimal Checklist

- API base URL env-ə çıxarılıb.
- Bütün public requests `lang` query parametri göndərir.
- Admin login `username/password` ilə edilir.
- Token bütün admin dəyişiklik request-lərində `Bearer` kimi gedir.
- Multipart endpoint-lərdə FormData istifadə olunur.
- Optional update file-ları yalnız seçiləndə göndərilir.
- Media URL-ləri response-dan olduğu kimi istifadə olunur.
- Settings admin `stringValue/mediaUrl/valueType` contract-ına uyğun qurulur.
- Public UI HTML content render edirsə sanitize edilir.
- List pagination olan endpoint-lərdə `paginationMetadata` nəzərə alınır.

