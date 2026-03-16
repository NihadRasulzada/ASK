using App.Core.Entities.Common;

namespace App.Core.Entities;

public class Director : SoftDeletableEntity
{
    public string ImageUrl { get; private set; }
    public string FullNameAz { get; private set; }
    public string FullNameEn { get; private set; }
    public string FullNameRu { get; private set; }
    public string DutyAz { get; private set; }
    public string DutyEn { get; private set; }
    public string DutyRu { get; private set; }

    // EF Core materialization
    private Director() : base(Guid.Empty, false)
    {
        ImageUrl = string.Empty;
        FullNameAz = string.Empty;
        FullNameEn = string.Empty;
        FullNameRu = string.Empty;
        DutyAz = string.Empty;
        DutyEn = string.Empty;
        DutyRu = string.Empty;
    }

    public Director(
        string imageUrl,
        string fullNameAz, string fullNameEn, string fullNameRu,
        string dutyAz, string dutyEn, string dutyRu)
        : base(Guid.NewGuid(), false)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));
        if (string.IsNullOrWhiteSpace(fullNameAz))
            throw new ArgumentException("Tam ad (AZ) boş ola bilməz.", nameof(fullNameAz));
        if (string.IsNullOrWhiteSpace(fullNameEn))
            throw new ArgumentException("Tam ad (EN) boş ola bilməz.", nameof(fullNameEn));
        if (string.IsNullOrWhiteSpace(fullNameRu))
            throw new ArgumentException("Tam ad (RU) boş ola bilməz.", nameof(fullNameRu));
        if (string.IsNullOrWhiteSpace(dutyAz))
            throw new ArgumentException("Vəzifə (AZ) boş ola bilməz.", nameof(dutyAz));
        if (string.IsNullOrWhiteSpace(dutyEn))
            throw new ArgumentException("Vəzifə (EN) boş ola bilməz.", nameof(dutyEn));
        if (string.IsNullOrWhiteSpace(dutyRu))
            throw new ArgumentException("Vəzifə (RU) boş ola bilməz.", nameof(dutyRu));

        ImageUrl = imageUrl;
        FullNameAz = fullNameAz;
        FullNameEn = fullNameEn;
        FullNameRu = fullNameRu;
        DutyAz = dutyAz;
        DutyEn = dutyEn;
        DutyRu = dutyRu;
    }

    /// <param name="imageUrl">Null ötürülərsə mövcud dəyər saxlanılır.</param>
    public void Update(
        string fullNameAz, string fullNameEn, string fullNameRu,
        string dutyAz, string dutyEn, string dutyRu)
    {

        if (string.IsNullOrWhiteSpace(fullNameAz))
            throw new ArgumentException("Tam ad (AZ) boş ola bilməz.", nameof(fullNameAz));
        if (string.IsNullOrWhiteSpace(fullNameEn))
            throw new ArgumentException("Tam ad (EN) boş ola bilməz.", nameof(fullNameEn));
        if (string.IsNullOrWhiteSpace(fullNameRu))
            throw new ArgumentException("Tam ad (RU) boş ola bilməz.", nameof(fullNameRu));
        if (string.IsNullOrWhiteSpace(dutyAz))
            throw new ArgumentException("Vəzifə (AZ) boş ola bilməz.", nameof(dutyAz));
        if (string.IsNullOrWhiteSpace(dutyEn))
            throw new ArgumentException("Vəzifə (EN) boş ola bilməz.", nameof(dutyEn));
        if (string.IsNullOrWhiteSpace(dutyRu))
            throw new ArgumentException("Vəzifə (RU) boş ola bilməz.", nameof(dutyRu));

        FullNameAz = fullNameAz;
        FullNameEn = fullNameEn;
        FullNameRu = fullNameRu;
        DutyAz = dutyAz;
        DutyEn = dutyEn;
        DutyRu = dutyRu;
    }

    public void UpdateImageUrl(string imageUrl)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));

        ImageUrl = imageUrl;
    }
}