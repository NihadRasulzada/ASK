using App.Core.Entities.Common;

namespace App.Core.Entities;

public class ForeignRepresentatives : BaseEntity
{
    public string CountryAz { get; private set; }
    public string CountryEn { get; private set; }
    public string CountryRu { get; private set; }
    public string FullNameAz { get; private set; }
    public string FullNameEn { get; private set; }
    public string FullNameRu { get; private set; }
    public string CompanyAz { get; private set; }
    public string CompanyEn { get; private set; }
    public string CompanyRu { get; private set; }
    public string DutyAz { get; private set; }
    public string DutyEn { get; private set; }
    public string DutyRu { get; private set; }

    private ForeignRepresentatives() : base(Guid.Empty)
    {
        CountryAz = string.Empty;
        CountryEn = string.Empty;
        CountryRu = string.Empty;
        FullNameAz = string.Empty;
        FullNameEn = string.Empty;
        FullNameRu = string.Empty;
        CompanyAz = string.Empty;
        CompanyEn = string.Empty;
        CompanyRu = string.Empty;
        DutyAz = string.Empty;
        DutyEn = string.Empty;
        DutyRu = string.Empty;
    }

    public ForeignRepresentatives(
        string countryAz, string countryEn, string countryRu,
        string fullNameAz, string fullNameEn, string fullNameRu,
        string companyAz, string companyEn, string companyRu,
        string dutyAz, string dutyEn, string dutyRu)
        : base(Guid.NewGuid())
    {
        if (string.IsNullOrWhiteSpace(countryAz)) throw new ArgumentException("Ölkə (AZ) boş ola bilməz.", nameof(countryAz));
        if (string.IsNullOrWhiteSpace(countryEn)) throw new ArgumentException("Ölkə (EN) boş ola bilməz.", nameof(countryEn));
        if (string.IsNullOrWhiteSpace(countryRu)) throw new ArgumentException("Ölkə (RU) boş ola bilməz.", nameof(countryRu));
        if (string.IsNullOrWhiteSpace(fullNameAz)) throw new ArgumentException("Tam ad (AZ) boş ola bilməz.", nameof(fullNameAz));
        if (string.IsNullOrWhiteSpace(fullNameEn)) throw new ArgumentException("Tam ad (EN) boş ola bilməz.", nameof(fullNameEn));
        if (string.IsNullOrWhiteSpace(fullNameRu)) throw new ArgumentException("Tam ad (RU) boş ola bilməz.", nameof(fullNameRu));
        if (string.IsNullOrWhiteSpace(companyAz)) throw new ArgumentException("Şirkət (AZ) boş ola bilməz.", nameof(companyAz));
        if (string.IsNullOrWhiteSpace(companyEn)) throw new ArgumentException("Şirkət (EN) boş ola bilməz.", nameof(companyEn));
        if (string.IsNullOrWhiteSpace(companyRu)) throw new ArgumentException("Şirkət (RU) boş ola bilməz.", nameof(companyRu));
        if (string.IsNullOrWhiteSpace(dutyAz)) throw new ArgumentException("Vəzifə (AZ) boş ola bilməz.", nameof(dutyAz));
        if (string.IsNullOrWhiteSpace(dutyEn)) throw new ArgumentException("Vəzifə (EN) boş ola bilməz.", nameof(dutyEn));
        if (string.IsNullOrWhiteSpace(dutyRu)) throw new ArgumentException("Vəzifə (RU) boş ola bilməz.", nameof(dutyRu));

        CountryAz = countryAz;
        CountryEn = countryEn;
        CountryRu = countryRu;
        FullNameAz = fullNameAz;
        FullNameEn = fullNameEn;
        FullNameRu = fullNameRu;
        CompanyAz = companyAz;
        CompanyEn = companyEn;
        CompanyRu = companyRu;
        DutyAz = dutyAz;
        DutyEn = dutyEn;
        DutyRu = dutyRu;
    }

    public void Update(
        string countryAz, string countryEn, string countryRu,
        string fullNameAz, string fullNameEn, string fullNameRu,
        string companyAz, string companyEn, string companyRu,
        string dutyAz, string dutyEn, string dutyRu)
    {
        if (string.IsNullOrWhiteSpace(countryAz)) throw new ArgumentException("Ölkə (AZ) boş ola bilməz.", nameof(countryAz));
        if (string.IsNullOrWhiteSpace(countryEn)) throw new ArgumentException("Ölkə (EN) boş ola bilməz.", nameof(countryEn));
        if (string.IsNullOrWhiteSpace(countryRu)) throw new ArgumentException("Ölkə (RU) boş ola bilməz.", nameof(countryRu));
        if (string.IsNullOrWhiteSpace(fullNameAz)) throw new ArgumentException("Tam ad (AZ) boş ola bilməz.", nameof(fullNameAz));
        if (string.IsNullOrWhiteSpace(fullNameEn)) throw new ArgumentException("Tam ad (EN) boş ola bilməz.", nameof(fullNameEn));
        if (string.IsNullOrWhiteSpace(fullNameRu)) throw new ArgumentException("Tam ad (RU) boş ola bilməz.", nameof(fullNameRu));
        if (string.IsNullOrWhiteSpace(companyAz)) throw new ArgumentException("Şirkət (AZ) boş ola bilməz.", nameof(companyAz));
        if (string.IsNullOrWhiteSpace(companyEn)) throw new ArgumentException("Şirkət (EN) boş ola bilməz.", nameof(companyEn));
        if (string.IsNullOrWhiteSpace(companyRu)) throw new ArgumentException("Şirkət (RU) boş ola bilməz.", nameof(companyRu));
        if (string.IsNullOrWhiteSpace(dutyAz)) throw new ArgumentException("Vəzifə (AZ) boş ola bilməz.", nameof(dutyAz));
        if (string.IsNullOrWhiteSpace(dutyEn)) throw new ArgumentException("Vəzifə (EN) boş ola bilməz.", nameof(dutyEn));
        if (string.IsNullOrWhiteSpace(dutyRu)) throw new ArgumentException("Vəzifə (RU) boş ola bilməz.", nameof(dutyRu));

        CountryAz = countryAz;
        CountryEn = countryEn;
        CountryRu = countryRu;
        FullNameAz = fullNameAz;
        FullNameEn = fullNameEn;
        FullNameRu = fullNameRu;
        CompanyAz = companyAz;
        CompanyEn = companyEn;
        CompanyRu = companyRu;
        DutyAz = dutyAz;
        DutyEn = dutyEn;
        DutyRu = dutyRu;
    }
}
