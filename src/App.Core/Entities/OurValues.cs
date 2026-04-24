using App.Core.Entities.Common;
using App.Core.Entities.Common.Cloudinary;

namespace App.Core.Entities;

public class OurValues : BaseEntity
{
    public string TitleAz { get; private set; }
    public string TitleEn { get; private set; }
    public string TitleRu { get; private set; }
    public CloudinaryURL ImageUrl { get; private set; }

    public Guid CloudinaryURLId { get; private set; }

    private OurValues() : base(Guid.Empty)
    {
    }

    public OurValues(
        string titleAz, string titleEn, string titleRu,
        CloudinaryURL imageUrl)
        : base(Guid.NewGuid())
    {
        if (string.IsNullOrWhiteSpace(titleAz)) throw new ArgumentException("Başlıq (AZ) boş ola bilməz.", nameof(titleAz));
        if (string.IsNullOrWhiteSpace(titleEn)) throw new ArgumentException("Başlıq (EN) boş ola bilməz.", nameof(titleEn));
        if (string.IsNullOrWhiteSpace(titleRu)) throw new ArgumentException("Başlıq (RU) boş ola bilməz.", nameof(titleRu));
        if (CloudinaryURL.IsNullOrEmpty(imageUrl)) throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));

        TitleAz = titleAz;
        TitleEn = titleEn;
        TitleRu = titleRu;
        ImageUrl = imageUrl;
    }

    public void Update(string titleAz, string titleEn, string titleRu)
    {
        if (string.IsNullOrWhiteSpace(titleAz)) throw new ArgumentException("Başlıq (AZ) boş ola bilməz.", nameof(titleAz));
        if (string.IsNullOrWhiteSpace(titleEn)) throw new ArgumentException("Başlıq (EN) boş ola bilməz.", nameof(titleEn));
        if (string.IsNullOrWhiteSpace(titleRu)) throw new ArgumentException("Başlıq (RU) boş ola bilməz.", nameof(titleRu));

        TitleAz = titleAz;
        TitleEn = titleEn;
        TitleRu = titleRu;
    }

    public void UpdateImageUrl(CloudinaryURL imageUrl)
    {
        if (CloudinaryURL.IsNullOrEmpty(imageUrl)) throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));
        ImageUrl = imageUrl;
    }
}
