using App.Core.Entities.Common;
using App.Core.Entities.Common.Cloudinary;

namespace App.Core.Entities;

public class Gallery : BaseEntity
{
    public CloudinaryURL ImageUrl { get; private set; }

    public Guid CloudinaryURLId { get; private set; }

    // FIX: EF Core materialization üçün parameterless constructor əlavə edildi
    private Gallery() : base(Guid.Empty)
    {
    }

    public Gallery(CloudinaryURL imageUrl) : base(Guid.NewGuid())
    {
        if (CloudinaryURL.IsNullOrEmpty(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));

        ImageUrl = imageUrl;
    }

    public void UpdateImageUrl(CloudinaryURL imageUrl)
    {
        if (CloudinaryURL.IsNullOrEmpty(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));

        ImageUrl = imageUrl;
    }
}