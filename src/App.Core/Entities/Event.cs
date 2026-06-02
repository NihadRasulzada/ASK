using App.Core.Entities.Common;
using App.Core.Entities.Common.Storage;

namespace App.Core.Entities;

public abstract class Event : SoftDeletableEntity
{
    public string TitleAz { get; private set; }
    public string TitleEn { get; private set; }
    public string TitleRu { get; private set; }
    public StoredFile TitleImageUrl { get; private set; }
    public string TextAz { get; private set; }
    public string TextEn { get; private set; }
    public string TextRu { get; private set; }

    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }

    public DateTime Created { get; private set; } = DateTime.UtcNow;

    protected Event() : base(Guid.Empty, false)
    {
    }

    protected Event(string titleAz, string titleEn, string titleRu, StoredFile titleImageUrl, string textAz, string textEn, string textRu, DateTime startDate, DateTime endDate)
        : base(Guid.NewGuid(), false)
    {
        if (string.IsNullOrWhiteSpace(titleAz))
            throw new ArgumentException("Başlıq boş ola bilməz.", nameof(titleAz));
        if (string.IsNullOrWhiteSpace(titleEn))
            throw new ArgumentException("Başlıq boş ola bilməz.", nameof(titleEn));
        if (string.IsNullOrWhiteSpace(titleRu))
            throw new ArgumentException("Başlıq boş ola bilməz.", nameof(titleRu));
        if (StoredFile.IsNullOrEmpty(titleImageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(titleImageUrl));
        if (string.IsNullOrWhiteSpace(textAz))
            throw new ArgumentException("Mətn boş ola bilməz.", nameof(textAz));
        if (string.IsNullOrWhiteSpace(textEn))
            throw new ArgumentException("Mətn boş ola bilməz.", nameof(textEn));
        if (string.IsNullOrWhiteSpace(textRu))
            throw new ArgumentException("Mətn boş ola bilməz.", nameof(textRu));
        if (endDate < startDate)
            throw new ArgumentException("Bitmə tarixi başlanğıc tarixindən əvvəl ola bilməz.", nameof(endDate));

        StartDate = startDate;
        EndDate = endDate;
        TitleAz = titleAz;
        TitleEn = titleEn;
        TitleRu = titleRu;
        TitleImageUrl = titleImageUrl;
        TextAz = textAz;
        TextEn = textEn;
        TextRu = textRu;
    }

    public void Update(string titleAz, string titleEn, string titleRu, string textAz, string textEn, string textRu, DateTime startDate, DateTime endDate)
    {
        if (string.IsNullOrWhiteSpace(titleAz)) throw new ArgumentException("Başlıq boş ola bilməz.", nameof(titleAz));
        if (string.IsNullOrWhiteSpace(titleEn)) throw new ArgumentException("Başlıq boş ola bilməz.", nameof(titleEn));
        if (string.IsNullOrWhiteSpace(titleRu)) throw new ArgumentException("Başlıq boş ola bilməz.", nameof(titleRu));
        if (string.IsNullOrWhiteSpace(textAz)) throw new ArgumentException("Mətn boş ola bilməz.", nameof(textAz));
        if (string.IsNullOrWhiteSpace(textEn)) throw new ArgumentException("Mətn boş ola bilməz.", nameof(textEn));
        if (string.IsNullOrWhiteSpace(textRu)) throw new ArgumentException("Mətn boş ola bilməz.", nameof(textRu));
        if (endDate < startDate) throw new ArgumentException("Bitmə tarixi başlanğıc tarixindən əvvəl ola bilməz.", nameof(endDate));

        StartDate = startDate;
        EndDate = endDate;
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
