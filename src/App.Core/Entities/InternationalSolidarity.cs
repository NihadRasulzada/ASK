using App.Core.Entities.Common;

namespace App.Core.Entities;

public class InternationalSolidarity : BaseEntity
{
    public string Link { get; private set; }
    public string IconUrl { get; private set; }

    private InternationalSolidarity() : base(Guid.Empty)
    {
        Link = string.Empty;
        IconUrl = string.Empty;
    }

    public InternationalSolidarity(string link, string iconUrl) : base(Guid.NewGuid())
    {
        if (string.IsNullOrWhiteSpace(link))
            throw new ArgumentException("Link boş ola bilməz.", nameof(link));
        if (string.IsNullOrWhiteSpace(iconUrl))
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

    public void UpdateIconUrl(string iconUrl)
    {
        if (string.IsNullOrWhiteSpace(iconUrl))
            throw new ArgumentException("İkon URL-i boş ola bilməz.", nameof(iconUrl));

        IconUrl = iconUrl;
    }
}
