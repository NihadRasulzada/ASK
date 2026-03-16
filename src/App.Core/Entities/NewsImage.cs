using App.Core.Entities.Common;

namespace App.Core.Entities;

public class NewsImage : BaseEntity
{
    public string ImageUrl { get; private set; }
    public Guid NewsId { get; private set; }
    public News News { get; private set; } = null!;

    private NewsImage() : base(Guid.Empty)
    {
        ImageUrl = string.Empty;
    }

    public NewsImage(string imageUrl, Guid newsId) : base(Guid.NewGuid())
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));
        if (newsId == Guid.Empty)
            throw new ArgumentException("NewsId boş ola bilməz.", nameof(newsId));

        ImageUrl = imageUrl;
        NewsId = newsId;
    }

    public void Update(string imageUrl)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));

        ImageUrl = imageUrl;
    }
}