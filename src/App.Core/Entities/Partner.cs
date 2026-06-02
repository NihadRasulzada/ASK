using App.Core.Entities.Common;
using App.Core.Entities.Common.Storage;

namespace App.Core.Entities;

public class Partner : BaseEntity
{
    public StoredFile ImageUrl { get; private set; }
    public string Site { get; private set; }

    private Partner() : base(Guid.Empty)
    {
    }

    public Partner(StoredFile imageUrl, string site) : base(Guid.NewGuid())
    {
        if (StoredFile.IsNullOrEmpty(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));
        if (string.IsNullOrWhiteSpace(site))
            throw new ArgumentException("Sayt URL-i boş ola bilməz.", nameof(site));

        ImageUrl = imageUrl;
        Site = site;
    }

    public void Update(string site)
    {
        if (string.IsNullOrWhiteSpace(site))
            throw new ArgumentException("Sayt URL-i boş ola bilməz.", nameof(site));

        Site = site;
    }

    public void UpdateImageUrl(StoredFile imageUrl)
    {
        if (StoredFile.IsNullOrEmpty(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));

        ImageUrl = imageUrl;
    }
}
