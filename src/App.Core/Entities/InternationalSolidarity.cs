using App.Core.Entities.Common;
using App.Core.Entities.Common.Storage;

namespace App.Core.Entities;

public class InternationalSolidarity : BaseEntity
{
    public string Link { get; private set; }
    public StoredFile IconUrl { get; private set; }

    private InternationalSolidarity() : base(Guid.Empty)
    {
    }

    public InternationalSolidarity(string link, StoredFile iconUrl) : base(Guid.NewGuid())
    {
        if (string.IsNullOrWhiteSpace(link))
            throw new ArgumentException("Link boş ola bilməz.", nameof(link));
        if (StoredFile.IsNullOrEmpty(iconUrl))
            throw new ArgumentException("İkon URL-i boş ola bilməz.", nameof(iconUrl));

        Link = link;
        IconUrl = iconUrl;
    }

    public void Update(string link)
    {
        if (string.IsNullOrWhiteSpace(link))
            throw new ArgumentException("Link boş ola bilməz.", nameof(link));

        Link = link;
    }

    public void UpdateIconUrl(StoredFile iconUrl)
    {
        if (StoredFile.IsNullOrEmpty(iconUrl))
            throw new ArgumentException("İkon URL-i boş ola bilməz.", nameof(iconUrl));

        IconUrl = iconUrl;
    }
}
