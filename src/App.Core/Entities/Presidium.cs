using App.Core.Entities.Common;

namespace App.Core.Entities;

public class Presidium : BaseEntity
{
    public string FullNameAz { get; private set; }
    public string FullNameEn { get; private set; }
    public string FullNameRu { get; private set; }
    public string PositionAz { get; private set; }
    public string PositionEn { get; private set; }
    public string PositionRu { get; private set; }
    public string ImageUrl { get; private set; }

    private Presidium() : base(Guid.Empty)
    {
        FullNameAz = string.Empty;
        FullNameEn = string.Empty;
        FullNameRu = string.Empty;
        PositionAz = string.Empty;
        PositionEn = string.Empty;
        PositionRu = string.Empty;
        ImageUrl = string.Empty;
    }

    public Presidium(
        string fullNameAz, string fullNameEn, string fullNameRu,
        string positionAz, string positionEn, string positionRu,
        string imageUrl)
        : base(Guid.NewGuid())
    {
        if (string.IsNullOrWhiteSpace(fullNameAz)) throw new ArgumentException("Tam ad (AZ) boş ola bilməz.", nameof(fullNameAz));
        if (string.IsNullOrWhiteSpace(fullNameEn)) throw new ArgumentException("Tam ad (EN) boş ola bilməz.", nameof(fullNameEn));
        if (string.IsNullOrWhiteSpace(fullNameRu)) throw new ArgumentException("Tam ad (RU) boş ola bilməz.", nameof(fullNameRu));
        if (string.IsNullOrWhiteSpace(positionAz)) throw new ArgumentException("Vəzifə (AZ) boş ola bilməz.", nameof(positionAz));
        if (string.IsNullOrWhiteSpace(positionEn)) throw new ArgumentException("Vəzifə (EN) boş ola bilməz.", nameof(positionEn));
        if (string.IsNullOrWhiteSpace(positionRu)) throw new ArgumentException("Vəzifə (RU) boş ola bilməz.", nameof(positionRu));
        if (string.IsNullOrWhiteSpace(imageUrl)) throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));

        FullNameAz = fullNameAz;
        FullNameEn = fullNameEn;
        FullNameRu = fullNameRu;
        PositionAz = positionAz;
        PositionEn = positionEn;
        PositionRu = positionRu;
        ImageUrl = imageUrl;
    }

    public void Update(
        string fullNameAz, string fullNameEn, string fullNameRu,
        string positionAz, string positionEn, string positionRu)
    {
        if (string.IsNullOrWhiteSpace(fullNameAz)) throw new ArgumentException("Tam ad (AZ) boş ola bilməz.", nameof(fullNameAz));
        if (string.IsNullOrWhiteSpace(fullNameEn)) throw new ArgumentException("Tam ad (EN) boş ola bilməz.", nameof(fullNameEn));
        if (string.IsNullOrWhiteSpace(fullNameRu)) throw new ArgumentException("Tam ad (RU) boş ola bilməz.", nameof(fullNameRu));
        if (string.IsNullOrWhiteSpace(positionAz)) throw new ArgumentException("Vəzifə (AZ) boş ola bilməz.", nameof(positionAz));
        if (string.IsNullOrWhiteSpace(positionEn)) throw new ArgumentException("Vəzifə (EN) boş ola bilməz.", nameof(positionEn));
        if (string.IsNullOrWhiteSpace(positionRu)) throw new ArgumentException("Vəzifə (RU) boş ola bilməz.", nameof(positionRu));

        FullNameAz = fullNameAz;
        FullNameEn = fullNameEn;
        FullNameRu = fullNameRu;
        PositionAz = positionAz;
        PositionEn = positionEn;
        PositionRu = positionRu;
    }

    public void UpdateImageUrl(string imageUrl)
    {
        if (string.IsNullOrWhiteSpace(imageUrl)) throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));
        ImageUrl = imageUrl;
    }
}
