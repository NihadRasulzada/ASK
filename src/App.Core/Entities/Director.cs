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

    private Director() : base(Guid.Empty, false)
    {
    }

    public Director(string imageUrl, string fullNameAz, string fullNameEn, string fullNameRu, string dutyAz, string dutyEn, string dutyRu) : base(Guid.NewGuid(), false)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));

        if (string.IsNullOrWhiteSpace(fullNameAz))
            throw new ArgumentException("Tam ad boş ola bilməz.", nameof(fullNameAz));

        if (string.IsNullOrWhiteSpace(fullNameEn))
            throw new ArgumentException("Tam ad boş ola bilməz.", nameof(fullNameEn));

        if (string.IsNullOrWhiteSpace(fullNameRu))
            throw new ArgumentException("Tam ad boş ola bilməz.", nameof(fullNameRu));

        if (string.IsNullOrWhiteSpace(dutyAz))
            throw new ArgumentException("Vəzifə boş ola bilməz.", nameof(dutyAz));

        if (string.IsNullOrWhiteSpace(dutyEn))
            throw new ArgumentException("Vəzifə boş ola bilməz.", nameof(dutyEn));

        if (string.IsNullOrWhiteSpace(dutyRu))
            throw new ArgumentException("Vəzifə boş ola bilməz.", nameof(dutyRu));

        ImageUrl = imageUrl;
        FullNameAz = fullNameAz;
        FullNameEn = fullNameEn;
        FullNameRu = fullNameRu;
        DutyAz = dutyAz;
        DutyEn = dutyEn;
        DutyRu = dutyRu;
    }

    public void Update(string? imageUrl, string fullNameAz, string fullNameEn, string fullNameRu, string dutyAz, string dutyEn, string dutyRu)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));

        if (string.IsNullOrWhiteSpace(fullNameAz))
            throw new ArgumentException("Tam ad boş ola bilməz.", nameof(fullNameAz));

        if (string.IsNullOrWhiteSpace(fullNameEn))
            throw new ArgumentException("Tam ad boş ola bilməz.", nameof(fullNameEn));

        if (string.IsNullOrWhiteSpace(fullNameRu))
            throw new ArgumentException("Tam ad boş ola bilməz.", nameof(fullNameRu));

        if (string.IsNullOrWhiteSpace(dutyAz))
            throw new ArgumentException("Vəzifə boş ola bilməz.", nameof(dutyAz));

        if (string.IsNullOrWhiteSpace(dutyEn))
            throw new ArgumentException("Vəzifə boş ola bilməz.", nameof(dutyEn));

        if (string.IsNullOrWhiteSpace(dutyRu))
            throw new ArgumentException("Vəzifə boş ola bilməz.", nameof(dutyRu));

        if (imageUrl is not null)
            ImageUrl = imageUrl;

        FullNameAz = fullNameAz;
        FullNameEn = fullNameEn;
        FullNameRu = fullNameRu;
        DutyAz = dutyAz;
        DutyEn = dutyEn;
        DutyRu = dutyRu;
    }
}
