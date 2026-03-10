using App.Core.Entities.Common;

namespace App.Core.Entities;

public class News : SoftDeletableEntity
{
    public string TitleImageUrl { get; private set; }
    public string NewsText { get; private set; }
    public IList<string> ImageUrls { get; private set; }

    private News() : base(Guid.Empty, false)
    {
        TitleImageUrl = string.Empty;
        NewsText = string.Empty;
        ImageUrls = new List<string>();
    }

    public News(string titleImageUrl, string newsText, IList<string> imageUrls) : base(Guid.NewGuid(), false)
    {
        if (string.IsNullOrWhiteSpace(titleImageUrl))
            throw new ArgumentException("Başlıq şəkli URL-i boş ola bilməz.", nameof(titleImageUrl));

        if (string.IsNullOrWhiteSpace(newsText))
            throw new ArgumentException("Xəbər mətni boş ola bilməz.", nameof(newsText));

        TitleImageUrl = titleImageUrl;
        NewsText = newsText;
        ImageUrls = imageUrls ?? new List<string>();
    }

    public void Update(string? titleImageUrl, string newsText, IList<string>? imageUrls)
    {
        if (string.IsNullOrWhiteSpace(newsText))
            throw new ArgumentException("Xəbər mətni boş ola bilməz.", nameof(newsText));

        if (titleImageUrl is not null)
            TitleImageUrl = titleImageUrl;

        NewsText = newsText;

        if (imageUrls is not null)
            ImageUrls = imageUrls;
    }
}
