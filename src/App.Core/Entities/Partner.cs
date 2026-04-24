using App.Core.Entities.Common;
using App.Core.Entities.Common.Cloudinary;

namespace App.Core.Entities;

public class Partner : BaseEntity
{
    public CloudinaryURL ImageUrl { get; private set; }
    public string Site { get; private set; }

    public Guid CloudinaryURLId { get; private set; }


    private Partner() : base(Guid.Empty)
    {
    }

    public Partner(CloudinaryURL imageUrl, string site) : base(Guid.NewGuid())
    {
        if (CloudinaryURL.IsNullOrEmpty(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));
        if (string.IsNullOrWhiteSpace(site))
            throw new ArgumentException("Sayt URL-i boş ola bilməz.", nameof(site));

        ImageUrl = imageUrl;
        Site = site;
    }

    /// <param name="imageUrl">Null ötürülərsə mövcud dəyər saxlanılır.</param>
    public void Update(string site)
    {
        if (string.IsNullOrWhiteSpace(site))
            throw new ArgumentException("Sayt URL-i boş ola bilməz.", nameof(site));

        Site = site;
    }

    public void UpdateImageUrl(CloudinaryURL imageUrl)
    {
        if (CloudinaryURL.IsNullOrEmpty(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));

        ImageUrl = imageUrl;
    }
}