using App.Core.Entities.Common;
using App.Core.Entities.Common.Cloudinary;

namespace App.Core.Entities;

public class NewsImage : BaseEntity
{
    public CloudinaryURL ImageUrl { get; private set; }
    public Guid NewsId { get; private set; }
    public News News { get; private set; } = null!;

    public Guid CloudinaryURLId { get; private set; }

    private NewsImage() : base(Guid.Empty)
    {
    }

    public NewsImage(CloudinaryURL imageUrl, Guid newsId) : base(Guid.NewGuid())
    {
        if (CloudinaryURL.IsNullOrEmpty(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));
        if (newsId == Guid.Empty)
            throw new ArgumentException("NewsId boş ola bilməz.", nameof(newsId));

        ImageUrl = imageUrl;
        NewsId = newsId;
    }

    public void UpdateImageUrl(CloudinaryURL imageUrl)
    {
        if (CloudinaryURL.IsNullOrEmpty(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));

        ImageUrl = imageUrl;
    }
}