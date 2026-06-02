using App.Core.Entities.Common;
using App.Core.Entities.Common.Storage;

namespace App.Core.Entities;

public class Announcement : BaseEntity
{
    public StoredFile TitleImageUrl { get; private set; }
    public string TitleAz { get; private set; }
    public string TitleEn { get; private set; }
    public string TitleRu { get; private set; }
    public string TextAz { get; private set; }
    public string TextEn { get; private set; }
    public string TextRu { get; private set; }

    public DateTime Created { get; private set; } = DateTime.UtcNow;

    public Announcement(string titleAz, string titleEn, string titleRu, StoredFile titleImageUrl, string textAz, string textEn, string textRu)
        : base(Guid.NewGuid())
    {
        if (string.IsNullOrWhiteSpace(titleAz)) throw new ArgumentException("Başlıq boş ola bilməz.", nameof(titleAz));
        if (string.IsNullOrWhiteSpace(titleEn)) throw new ArgumentException("Başlıq boş ola bilməz.", nameof(titleEn));
        if (string.IsNullOrWhiteSpace(titleRu)) throw new ArgumentException("Başlıq boş ola bilməz.", nameof(titleRu));
        if (StoredFile.IsNullOrEmpty(titleImageUrl)) throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(titleImageUrl));
        if (string.IsNullOrWhiteSpace(textAz)) throw new ArgumentException("Mətn boş ola bilməz.", nameof(textAz));
        if (string.IsNullOrWhiteSpace(textEn)) throw new ArgumentException("Mətn boş ola bilməz.", nameof(textEn));
        if (string.IsNullOrWhiteSpace(textRu)) throw new ArgumentException("Mətn boş ola bilməz.", nameof(textRu));

        TitleAz = titleAz;
        TitleEn = titleEn;
        TitleRu = titleRu;
        TitleImageUrl = titleImageUrl;
        TextAz = textAz;
        TextEn = textEn;
        TextRu = textRu;
    }

    private Announcement() : base(Guid.Empty)
    {
        TitleAz = string.Empty;
        TitleEn = string.Empty;
        TitleRu = string.Empty;
        TextAz = string.Empty;
        TextEn = string.Empty;
        TextRu = string.Empty;
    }

    public void Update(string titleAz, string titleEn, string titleRu, string textAz, string textEn, string textRu)
    {
        if (string.IsNullOrWhiteSpace(titleAz)) throw new ArgumentException("Başlıq boş ola bilməz.", nameof(titleAz));
        if (string.IsNullOrWhiteSpace(titleEn)) throw new ArgumentException("Başlıq boş ola bilməz.", nameof(titleEn));
        if (string.IsNullOrWhiteSpace(titleRu)) throw new ArgumentException("Başlıq boş ola bilməz.", nameof(titleRu));
        if (string.IsNullOrWhiteSpace(textAz)) throw new ArgumentException("Mətn boş ola bilməz.", nameof(textAz));
        if (string.IsNullOrWhiteSpace(textEn)) throw new ArgumentException("Mətn boş ola bilməz.", nameof(textEn));
        if (string.IsNullOrWhiteSpace(textRu)) throw new ArgumentException("Mətn boş ola bilməz.", nameof(textRu));

        TitleAz = titleAz;
        TitleEn = titleEn;
        TitleRu = titleRu;
        TextAz = textAz;
        TextEn = textEn;
        TextRu = textRu;
    }

    public void UpdateImageUrl(StoredFile titleImageUrl)
    {
        if (StoredFile.IsNullOrEmpty(titleImageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(titleImageUrl));

        TitleImageUrl = titleImageUrl;
    }
}
