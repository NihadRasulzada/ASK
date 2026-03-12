using App.Core.Entities.Common;

namespace App.Core.Entities;

public class News : SoftDeletableEntity
{
    public string TitleImageUrl { get; private set; }
    public string NewsTextAz { get; private set; }
    public string NewsTextEn { get; private set; }
    public string NewsTextRu { get; private set; }
    public IList<string> ImageUrls { get; private set; }

    private News() : base(Guid.Empty, false)
    {
        ImageUrls = new List<string>();
    }

    public News(
        string titleImageUrl,
        string newsTextAz,
        string newsTextEn,
        string newsTextRu,
        IList<string>? imageUrls)
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
        ImageUrls = imageUrls ?? new List<string>();
    }

    public void Update(
        string? titleImageUrl,
        string newsTextAz,
        string newsTextEn,
        string newsTextRu,
        IList<string>? imageUrls)
    {
        if (string.IsNullOrWhiteSpace(newsTextAz))
            throw new ArgumentException("AZ news text cannot be empty", nameof(newsTextAz));

        if (string.IsNullOrWhiteSpace(newsTextEn))
            throw new ArgumentException("EN news text cannot be empty", nameof(newsTextEn));

        if (string.IsNullOrWhiteSpace(newsTextRu))
            throw new ArgumentException("RU news text cannot be empty", nameof(newsTextRu));

        if (titleImageUrl is not null)
            TitleImageUrl = titleImageUrl;

        NewsTextAz = newsTextAz;
        NewsTextEn = newsTextEn;
        NewsTextRu = newsTextRu;

        if (imageUrls is not null)
            ImageUrls = imageUrls;
    }
}