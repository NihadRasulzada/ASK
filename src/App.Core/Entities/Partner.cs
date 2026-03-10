using App.Core.Entities.Common;

namespace App.Core.Entities;

public class Partner : BaseEntity
{
    public string ImageUrl { get; private set; }
    public string Site { get; private set; }

    private Partner() : base(Guid.Empty)
    {
        ImageUrl = string.Empty;
        Site = string.Empty;
    }

    public Partner(string imageUrl, string site) : base(Guid.NewGuid())
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));

        if (string.IsNullOrWhiteSpace(site))
            throw new ArgumentException("Sayt URL-i boş ola bilməz.", nameof(site));

        ImageUrl = imageUrl;
        Site = site;
    }

    public void Update(string? imageUrl, string site)
    {
        if (string.IsNullOrWhiteSpace(site))
            throw new ArgumentException("Sayt URL-i boş ola bilməz.", nameof(site));

        if (imageUrl is not null)
            ImageUrl = imageUrl;

        Site = site;
    }
}