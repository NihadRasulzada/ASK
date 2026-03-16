using App.Core.Entities.Common;

namespace App.Core.Entities;

public class Gallery : BaseEntity
{
    public string ImageUrl { get; private set; }

    // FIX: EF Core materialization üçün parameterless constructor əlavə edildi
    private Gallery() : base(Guid.Empty)
    {
        ImageUrl = string.Empty;
    }

    public Gallery(string imageUrl) : base(Guid.NewGuid())
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));

        ImageUrl = imageUrl;
    }

    public void Update(string imageUrl)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));

        ImageUrl = imageUrl;
    }
}