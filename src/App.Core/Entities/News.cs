using App.Core.Entities.Common;

namespace App.Core.Entities;

public class News : SoftDeletableEntity
{
    public string TitleImageUrl { get; private set; }
    public string NewsTextAz { get; private set; }
    public string NewsTextEn { get; private set; }
    public string NewsTextRu { get; private set; }
    public ICollection<string> ImageUrls { get; set; }

    private News() : base(Guid.Empty, false)
    {
    }

    public News(
        string titleImageUrl,
        string newsTextAz,
        string newsTextEn,
        string newsTextRu)
        : base(Guid.NewGuid(), false)
    {
        if (string.IsNullOrWhiteSpace(titleImageUrl))
            throw new ArgumentException("Title image url cannot be empty", nameof(titleImageUrl));

        if (string.IsNullOrWhiteSpace(newsTextAz))
            throw new ArgumentException("AZ news text cannot be empty", nameof(newsTextAz));

        if (string.IsNullOrWhiteSpace(newsTextEn))
            throw new ArgumentException("EN news text cannot be empty", nameof(newsTextEn));

        if (string.IsNullOrWhiteSpace(newsTextRu))
            throw new ArgumentException("RU news text cannot be empty", nameof(newsTextRu));

        TitleImageUrl = titleImageUrl;
        NewsTextAz = newsTextAz;
        NewsTextEn = newsTextEn;
        NewsTextRu = newsTextRu;
    }

    public void Update(
        string newsTextAz,
        string newsTextEn,
        string newsTextRu)
    {
        if (string.IsNullOrWhiteSpace(newsTextAz))
            throw new ArgumentException("AZ news text cannot be empty", nameof(newsTextAz));

        if (string.IsNullOrWhiteSpace(newsTextEn))
            throw new ArgumentException("EN news text cannot be empty", nameof(newsTextEn));

        if (string.IsNullOrWhiteSpace(newsTextRu))
            throw new ArgumentException("RU news text cannot be empty", nameof(newsTextRu));

        NewsTextAz = newsTextAz;
        NewsTextEn = newsTextEn;
        NewsTextRu = newsTextRu;
    }
}