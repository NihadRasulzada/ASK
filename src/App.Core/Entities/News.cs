using App.Core.Entities.Common;
using App.Core.Entities.Common.Cloudinary;

namespace App.Core.Entities;

// FIX: ICollection<string> ImageUrls silindi — NewsImage entity-si ilə
//      düzgün relational əlaqə istifadə olunur. İkiqat yanaşma ziddiyyət yaradır.
public class News : SoftDeletableEntity
{
    public CloudinaryURL TitleImageUrl { get; private set; }
    public string NewsTextAz { get; private set; }
    public string NewsTextEn { get; private set; }
    public string NewsTextRu { get; private set; }
    public DateTime CreateDate { get; private set; }

    public Guid CloudinaryURLId { get; private set; }

    public ICollection<NewsImage> Images { get; private set; } = new List<NewsImage>();

    private News() : base(Guid.Empty, false)
    {
    }

    public News(
        CloudinaryURL titleImageUrl,
        string newsTextAz,
        string newsTextEn,
        string newsTextRu)
        : base(Guid.NewGuid(), false)
    {
        if (CloudinaryURL.IsNullOrEmpty(titleImageUrl))
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

    public void UpdateImageUrl(CloudinaryURL titleImageUrl)
    {
        if (CloudinaryURL.IsNullOrEmpty(titleImageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(titleImageUrl));

        TitleImageUrl = titleImageUrl;
    }

}