using App.Core.Entities.Common;
using App.Core.Entities.Common.Storage;

namespace App.Core.Entities;

public class News : SoftDeletableEntity
{
    public StoredFile TitleImageUrl { get; private set; }
    public string TitleEn { get; private set; }
    public string TitleAz { get; private set; }
    public string TitleRu { get; private set; }
    public string NewsTextAz { get; private set; }
    public string NewsTextEn { get; private set; }
    public string NewsTextRu { get; private set; }
    public DateTime CreateDate { get; private set; }

    public ICollection<NewsImage> Images { get; private set; } = new List<NewsImage>();

    private News()
        : base(Guid.Empty, false) { }

    public News(
        StoredFile titleImageUrl,
        string newsTextAz,
        string newsTextEn,
        string newsTextRu,
        string titleAz,
        string titleEn,
        string titleRu
    )
        : base(Guid.NewGuid(), false)
    {
        if (StoredFile.IsNullOrEmpty(titleImageUrl))
            throw new ArgumentException("Title image url cannot be empty.", nameof(titleImageUrl));
        if (string.IsNullOrWhiteSpace(newsTextAz))
            throw new ArgumentException("AZ news text cannot be empty.", nameof(newsTextAz));
        if (string.IsNullOrWhiteSpace(newsTextEn))
            throw new ArgumentException("EN news text cannot be empty.", nameof(newsTextEn));
        if (string.IsNullOrWhiteSpace(newsTextRu))
            throw new ArgumentException("RU news text cannot be empty.", nameof(newsTextRu));
        if (string.IsNullOrWhiteSpace(titleAz))
            throw new ArgumentException("AZ title cannot be empty.", nameof(titleAz));
        if (string.IsNullOrWhiteSpace(titleEn))
            throw new ArgumentException("EN title cannot be empty.", nameof(titleEn));
        if (string.IsNullOrWhiteSpace(titleRu))
            throw new ArgumentException("RU title cannot be empty.", nameof(titleRu));

        TitleImageUrl = titleImageUrl;
        NewsTextAz = newsTextAz;
        NewsTextEn = newsTextEn;
        NewsTextRu = newsTextRu;
        TitleAz = titleAz;
        TitleEn = titleEn;
        TitleRu = titleRu;
        CreateDate = DateTime.UtcNow;
    }

    public void Update(
        string newsTextAz,
        string newsTextEn,
        string newsTextRu,
        string titleAz,
        string titleEn,
        string titleRu
    )
    {
        if (string.IsNullOrWhiteSpace(newsTextAz))
            throw new ArgumentException("AZ news text cannot be empty.", nameof(newsTextAz));
        if (string.IsNullOrWhiteSpace(newsTextEn))
            throw new ArgumentException("EN news text cannot be empty.", nameof(newsTextEn));
        if (string.IsNullOrWhiteSpace(newsTextRu))
            throw new ArgumentException("RU news text cannot be empty.", nameof(newsTextRu));
        if (string.IsNullOrWhiteSpace(titleAz))
            throw new ArgumentException("AZ title cannot be empty.", nameof(titleAz));
        if (string.IsNullOrWhiteSpace(titleEn))
            throw new ArgumentException("EN title cannot be empty.", nameof(titleEn));
        if (string.IsNullOrWhiteSpace(titleRu))
            throw new ArgumentException("RU title cannot be empty.", nameof(titleRu));

        NewsTextAz = newsTextAz;
        NewsTextEn = newsTextEn;
        NewsTextRu = newsTextRu;
        TitleAz = titleAz;
        TitleEn = titleEn;
        TitleRu = titleRu;
    }

    public void UpdateImageUrl(StoredFile titleImageUrl)
    {
        if (StoredFile.IsNullOrEmpty(titleImageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(titleImageUrl));

        TitleImageUrl = titleImageUrl;
    }
}