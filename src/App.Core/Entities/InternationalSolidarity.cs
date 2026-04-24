using App.Core.Entities.Common;
using App.Core.Entities.Common.Cloudinary;

namespace App.Core.Entities;

public class InternationalSolidarity : BaseEntity
{
    public string Link { get; private set; }
    public CloudinaryURL IconUrl { get; private set; }

    public Guid CloudinaryURLId { get; private set; }

    private InternationalSolidarity() : base(Guid.Empty)
    {
    }

    public InternationalSolidarity(string link, CloudinaryURL iconUrl) : base(Guid.NewGuid())
    {
        if (string.IsNullOrWhiteSpace(link))
            throw new ArgumentException("Link boş ola bilməz.", nameof(link));
        if (CloudinaryURL.IsNullOrEmpty(iconUrl))
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

    public void UpdateIconUrl(CloudinaryURL iconUrl)
    {
        if (CloudinaryURL.IsNullOrEmpty(iconUrl))
            throw new ArgumentException("İkon URL-i boş ola bilməz.", nameof(iconUrl));

        IconUrl = iconUrl;
    }
}
