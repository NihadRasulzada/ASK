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

    /// <param name="imageUrl">Null ötürülərsə mövcud dəyər saxlanılır.</param>
    public void Update(string site)
    {
        if (string.IsNullOrWhiteSpace(site))
            throw new ArgumentException("Sayt URL-i boş ola bilməz.", nameof(site));

        Site = site;
    }

    public void UpdateImageUrl(string imageUrl)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));

        ImageUrl = imageUrl;
    }
}