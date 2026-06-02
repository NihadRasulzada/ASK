using App.Core.Entities.Common;
using App.Core.Entities.Common.Storage;

namespace App.Core.Entities;

public class News : SoftDeletableEntity
{
    public StoredFile TitleImageUrl { get; private set; }
    public string NewsTextAz { get; private set; }
    public string NewsTextEn { get; private set; }
    public string NewsTextRu { get; private set; }
    public DateTime CreateDate { get; private set; }

    public ICollection<NewsImage> Images { get; private set; } = new List<NewsImage>();

    private News() : base(Guid.Empty, false)
    {
    }

    public News(
        StoredFile titleImageUrl,
        string newsTextAz,
        string newsTextEn,
        string newsTextRu)
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

        TitleImageUrl = titleImageUrl;
        NewsTextAz = newsTextAz;
        NewsTextEn = newsTextEn;
        NewsTextRu = newsTextRu;
        CreateDate = DateTime.UtcNow;
    }

    public void Update(
        string newsTextAz,
        string newsTextEn,
        string newsTextRu)
    {
        if (string.IsNullOrWhiteSpace(newsTextAz))
            throw new ArgumentException("AZ news text cannot be empty.", nameof(newsTextAz));
        if (string.IsNullOrWhiteSpace(newsTextEn))
            throw new ArgumentException("EN news text cannot be empty.", nameof(newsTextEn));
        if (string.IsNullOrWhiteSpace(newsTextRu))
            throw new ArgumentException("RU news text cannot be empty.", nameof(newsTextRu));

        NewsTextAz = newsTextAz;
        NewsTextEn = newsTextEn;
        NewsTextRu = newsTextRu;
    }

    public void UpdateImageUrl(StoredFile titleImageUrl)
    {
        if (StoredFile.IsNullOrEmpty(titleImageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(titleImageUrl));

        TitleImageUrl = titleImageUrl;
    }
}
