# ASK API Pagination Guide

Bu sənəd frontend developer-lər üçündür. Məqsəd odur ki, public sayt və admin panel list endpoint-lərini eyni qayda ilə istifadə etsin, artıq əlavə sual lazım olmasın.

## Qısa Contract

Bütün list/siyahı endpoint-lərində pagination query parametrləri istifadə olunur:

```http
GET /api/News?pageIndex=1&pageSize=50&lang=az
```

Parametrlər:

| Parametr | Default | Limit | İzah |
| --- | ---: | ---: | --- |
| `pageIndex` | `1` | minimum `1` | Səhifə nömrəsi, 1-dən başlayır |
| `pageSize` | `10` | `1-100` | Bir səhifədə neçə item gələcək |
| `lang` | `az` | `az/en/ru` | Lokalizasiya üçün |

Backend invalid dəyərləri normalize edir:

- `pageIndex=0` və ya mənfi dəyər göndərilsə, `1` kimi qəbul olunur.
- `pageSize=0` və ya mənfi dəyər göndərilsə, `1` kimi qəbul olunur.
- `pageSize=100`-dən böyük göndərilsə, `100` kimi qəbul olunur.

## Response Forması

Paginated response həmişə bu formadadır:

```json
{
  "success": true,
  "message": "News retrieved successfully",
  "data": [
    {
      "id": "..."
    }
  ],
  "paginationMetadata": {
    "pageIndex": 1,
    "pageSize": 50,
    "totalCount": 997,
    "totalPages": 20,
    "hasPreviousPage": false,
    "hasNextPage": true
  }
}
```

Frontend yalnız `data`-nı render etməməlidir. `paginationMetadata` mütləq saxlanmalıdır.

## Əsas Qaydalar

1. Public və admin list-lərində eyni query contract var: `pageIndex`, `pageSize`.
2. Detail endpoint-lər pagination qəbul etmir: `/api/News/{id}` kimi.
3. Create/update/delete endpoint-lər pagination qəbul etmir.
4. `lang` pagination-dan ayrıcadır və həmişə saxlanmalıdır.
5. Filter/search sonradan əlavə olunsa belə, səhifə dəyişəndə query parametrlər birlikdə saxlanmalıdır.
6. Frontend `pageSize` üçün ən praktik dəyərlər: `10`, `20`, `50`, `100`.
7. Infinite scroll üçün `hasNextPage` false olana qədər növbəti səhifə çağırılır.

## Public Endpoint-lər

Bu public endpoint-lər paginated response qaytarır:

| Bölmə | Endpoint nümunəsi |
| --- | --- |
| News | `/api/News?pageIndex=1&pageSize=50&lang=az` |
| Announcements | `/api/Announcement?pageIndex=1&pageSize=10&lang=az` |
| Services | `/api/Service?pageIndex=1&pageSize=10&lang=az` |
| Publications | `/api/Publication?pageIndex=1&pageSize=10&lang=az` |
| Exhibition | `/api/Exhibition?pageIndex=1&pageSize=10&lang=az` |
| Exhibition all | `/api/Exhibition/all?pageIndex=1&pageSize=10&lang=az` |
| Training | `/api/Training?pageIndex=1&pageSize=10&lang=az` |
| Training all | `/api/Training/all?pageIndex=1&pageSize=10&lang=az` |
| Business Forum | `/api/BusinessForum?pageIndex=1&pageSize=10&lang=az` |
| Directors | `/api/Director?pageIndex=1&pageSize=10&lang=az` |
| Presidium | `/api/Presidium?pageIndex=1&pageSize=10&lang=az` |
| President | `/api/President?pageIndex=1&pageSize=10&lang=az` |
| Management | `/api/Management?pageIndex=1&pageSize=10&lang=az` |
| Committees | `/api/Committee?pageIndex=1&pageSize=10&lang=az` |
| District Representatives | `/api/DistrictRepresentatives?pageIndex=1&pageSize=10&lang=az` |
| Foreign Representatives | `/api/ForeignRepresentatives?pageIndex=1&pageSize=10&lang=az` |
| Our Values | `/api/OurValues?pageIndex=1&pageSize=10&lang=az` |
| Gallery | `/api/Gallery?pageIndex=1&pageSize=20` |
| Partners | `/api/Partner?pageIndex=1&pageSize=20` |
| Videos | `/api/Video?pageIndex=1&pageSize=10` |
| Useful Links | `/api/UsefulLink?pageIndex=1&pageSize=10&lang=az` |
| FAQ | `/api/FAQ?pageIndex=1&pageSize=10&lang=az` |
| International Solidarity | `/api/InternationalSolidarity?pageIndex=1&pageSize=10` |

## Admin Endpoint-lər

Admin list endpoint-ləri də eyni metadata ilə gəlir:

| Bölmə | Endpoint nümunəsi |
| --- | --- |
| Services including deleted | `/api/Service/including-deleted?pageIndex=1&pageSize=20&lang=az` |
| News including deleted | `/api/News/including-deleted?pageIndex=1&pageSize=20&lang=az` |
| Directors all | `/api/Director/all?pageIndex=1&pageSize=20&lang=az` |
| Useful links all | `/api/UsefulLink/all?pageIndex=1&pageSize=20&lang=az` |
| FAQ all | `/api/FAQ/all?pageIndex=1&pageSize=20&lang=az` |
| FAQ inquiries | `/api/FAQ/inquiry?pageIndex=1&pageSize=20` |
| Exhibition all date data | `/api/Exhibition/alldatedata?pageIndex=1&pageSize=20&lang=az` |
| Exhibition all | `/api/Exhibition/all?pageIndex=1&pageSize=20&lang=az` |
| Training all date data | `/api/Training/alldatedata?pageIndex=1&pageSize=20&lang=az` |
| Training all | `/api/Training/all?pageIndex=1&pageSize=20&lang=az` |
| News images | `/api/NewsImage?pageIndex=1&pageSize=20` |
| Settings | `/api/Setting?pageIndex=1&pageSize=50` |

Admin request-lərdə auth header olduğu kimi qalır:

```http
Authorization: Bearer <token>
```

## Pagination Olmayan Endpoint-lər

Bunlar list pagination contract-ına daxil deyil:

| Endpoint | Səbəb |
| --- | --- |
| `/api/Auth/*` | Auth əməliyyatlarıdır |
| `/api/Media/{path}` | Fayl stream edir |
| `/api/Calendar?date=yyyy-MM-dd` | Seçilmiş gün/ay üçün xüsusi response qaytarır |
| `/api/Currency` | Xüsusi valyuta response-u qaytarır, yalnız 4 rate var |
| `/{id}` detail endpoint-ləri | Tək item qaytarır |
| `POST/PUT/PATCH/DELETE` | Mutation endpoint-ləridir |

## TypeScript Tipləri

Frontenddə bütün paginated endpoint-lər üçün eyni generic tip istifadə edin:

```ts
export type PaginationMetadata = {
  pageIndex: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
};

export type PaginatedResponse<T> = {
  success: boolean;
  message: string;
  data: T[];
  paginationMetadata: PaginationMetadata;
};
```

News tipi nümunə:

```ts
export type NewsItem = {
  id: string;
  titleImageUrl: string;
  title: string;
  newsText: string;
  imageUrls: string[];
  isDeactive: boolean;
  createDate: string;
};
```

## API Helper

```ts
const API = process.env.NEXT_PUBLIC_API_BASE_URL!;

type ListParams = {
  pageIndex?: number;
  pageSize?: number;
  lang?: "az" | "en" | "ru";
  token?: string;
};

export async function getPaginated<T>(
  path: string,
  params: ListParams = {}
): Promise<PaginatedResponse<T>> {
  const search = new URLSearchParams();
  search.set("pageIndex", String(params.pageIndex ?? 1));
  search.set("pageSize", String(params.pageSize ?? 10));

  if (params.lang) {
    search.set("lang", params.lang);
  }

  const res = await fetch(`${API}${path}?${search.toString()}`, {
    headers: params.token
      ? { Authorization: `Bearer ${params.token}` }
      : undefined,
    cache: "no-store"
  });

  if (!res.ok) {
    throw new Error(`API failed: ${res.status}`);
  }

  return res.json();
}
```

İstifadə:

```ts
const news = await getPaginated<NewsItem>("/api/News", {
  pageIndex: 1,
  pageSize: 50,
  lang: "az"
});

console.log(news.data);
console.log(news.paginationMetadata.totalCount);
```

## Page Button UI

Normal pagination üçün state:

```ts
const [pageIndex, setPageIndex] = useState(1);
const [pageSize, setPageSize] = useState(10);
```

Request:

```ts
const response = await getPaginated<NewsItem>("/api/News", {
  pageIndex,
  pageSize,
  lang
});
```

Buttons:

```tsx
<button
  disabled={!response.paginationMetadata.hasPreviousPage}
  onClick={() => setPageIndex((page) => Math.max(1, page - 1))}
>
  Previous
</button>

<span>
  {response.paginationMetadata.pageIndex} / {response.paginationMetadata.totalPages}
</span>

<button
  disabled={!response.paginationMetadata.hasNextPage}
  onClick={() => setPageIndex((page) => page + 1)}
>
  Next
</button>
```

Page size dəyişəndə `pageIndex` yenidən `1` edilməlidir:

```ts
function onPageSizeChange(nextPageSize: number) {
  setPageSize(nextPageSize);
  setPageIndex(1);
}
```

## Infinite Scroll

Infinite scroll üçün `hasNextPage` istifadə edin:

```ts
const [items, setItems] = useState<NewsItem[]>([]);
const [pageIndex, setPageIndex] = useState(1);
const [hasNextPage, setHasNextPage] = useState(true);

async function loadMore() {
  if (!hasNextPage) return;

  const response = await getPaginated<NewsItem>("/api/News", {
    pageIndex,
    pageSize: 20,
    lang
  });

  setItems((current) => [...current, ...response.data]);
  setHasNextPage(response.paginationMetadata.hasNextPage);
  setPageIndex((page) => page + 1);
}
```

Dil dəyişəndə infinite scroll state sıfırlanmalıdır:

```ts
setItems([]);
setPageIndex(1);
setHasNextPage(true);
```

## Admin Table Qaydası

Admin table-lərdə backend metadata source of truth olmalıdır.

Table state:

```ts
type TableState = {
  pageIndex: number;
  pageSize: number;
};
```

Recommended page size options:

```ts
const PAGE_SIZE_OPTIONS = [10, 20, 50, 100];
```

Admin list request:

```ts
const response = await getPaginated<NewsItem>("/api/News/including-deleted", {
  pageIndex: table.pageIndex,
  pageSize: table.pageSize,
  lang,
  token
});
```

Delete/create/update əməliyyatından sonra cari səhifəni refresh edin:

```ts
await deleteNews(id);
await refetchCurrentPage();
```

Əgər delete nəticəsində səhifə boş qalarsa və `pageIndex > 1`-dirsə, bir səhifə geri gedin:

```ts
if (response.data.length === 0 && response.paginationMetadata.pageIndex > 1) {
  setPageIndex(response.paginationMetadata.pageIndex - 1);
}
```

## URL State

Public saytda list səhifələrində pagination URL-də saxlanmalıdır:

```text
/news?page=2&pageSize=20
```

API-yə map:

```ts
const apiPageIndex = Number(searchParams.get("page") ?? "1");
const apiPageSize = Number(searchParams.get("pageSize") ?? "20");
```

Sonra:

```ts
getPaginated<NewsItem>("/api/News", {
  pageIndex: apiPageIndex,
  pageSize: apiPageSize,
  lang
});
```

Frontend route-da `page`, API-də `pageIndex` istifadə etmək olar, amma helper içində bunu aydın map edin.

## Vacib Breaking Change

Əvvəl bəzi endpoint-lər belə response qaytarırdı:

```json
{
  "success": true,
  "data": []
}
```

İndi list endpoint-lər belə qaytaracaq:

```json
{
  "success": true,
  "data": [],
  "paginationMetadata": {
    "pageIndex": 1,
    "pageSize": 10,
    "totalCount": 100,
    "totalPages": 10,
    "hasPreviousPage": false,
    "hasNextPage": true
  }
}
```

Yəni frontenddə list oxuyan yerlərdə aşağıdakılar yoxlanmalıdır:

- `response.data` hələ də array-dir.
- Amma pagination button/infinite scroll üçün `response.paginationMetadata` istifadə olunmalıdır.
- Köhnə kod `data.length === total` kimi davranırdısa, onu `paginationMetadata.totalCount` ilə əvəz edin.

## Test Nümunələri

50 xəbər:

```bash
curl -s "https://askapi.isaaholic.cyou/api/News?pageIndex=1&pageSize=50&lang=az" | jq '.data | length'
```

Metadata:

```bash
curl -s "https://askapi.isaaholic.cyou/api/News?pageIndex=1&pageSize=50&lang=az" | jq '.paginationMetadata'
```

2-ci səhifə:

```bash
curl -s "https://askapi.isaaholic.cyou/api/News?pageIndex=2&pageSize=50&lang=az" | jq '.data[0]'
```

Admin including-deleted:

```bash
curl -s "https://askapi.isaaholic.cyou/api/News/including-deleted?pageIndex=1&pageSize=50&lang=az" \
  -H "Authorization: Bearer TOKEN" \
  | jq '.paginationMetadata'
```

## Frontend Checklist

- Bütün list request-lərə `pageIndex` və `pageSize` əlavə olunub.
- Public lokalizə olunan list request-lərdə `lang` saxlanılır.
- Admin list request-lərdə `Authorization` header saxlanılır.
- `paginationMetadata` TypeScript tipinə əlavə olunub.
- Page size dəyişəndə `pageIndex` 1-ə reset olunur.
- Delete sonrası boş səhifə qalarsa əvvəlki səhifəyə qayıdılır.
- Infinite scroll `hasNextPage` ilə dayanır.
- `totalCount` UI-da total item sayı üçün istifadə olunur.
- `totalPages` page button sayı üçün istifadə olunur.
- `pageSize` maksimum 100 qəbul edilir.

