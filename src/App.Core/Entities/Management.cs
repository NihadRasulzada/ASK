using App.Core.Entities.Common;

namespace App.Core.Entities;

public class Management : BaseEntity
{
    public string FullNameAz { get; private set; }
    public string FullNameEn { get; private set; }
    public string FullNameRu { get; private set; }
    public string CompanyAz { get; private set; }
    public string CompanyEn { get; private set; }
    public string CompanyRu { get; private set; }

    private Management() : base(Guid.Empty)
    {
        FullNameAz = string.Empty;
        FullNameEn = string.Empty;
        FullNameRu = string.Empty;
        CompanyAz = string.Empty;
        CompanyEn = string.Empty;
        CompanyRu = string.Empty;
    }

    public Management(
        string fullNameAz, string fullNameEn, string fullNameRu,
        string companyAz, string companyEn, string companyRu)
        : base(Guid.NewGuid())
    {
        if (string.IsNullOrWhiteSpace(fullNameAz))
            throw new ArgumentException("Tam ad (AZ) boş ola bilməz.", nameof(fullNameAz));
        if (string.IsNullOrWhiteSpace(fullNameEn))
            throw new ArgumentException("Tam ad (EN) boş ola bilməz.", nameof(fullNameEn));
        if (string.IsNullOrWhiteSpace(fullNameRu))
            throw new ArgumentException("Tam ad (RU) boş ola bilməz.", nameof(fullNameRu));
        if (string.IsNullOrWhiteSpace(companyAz))
            throw new ArgumentException("Şirkət (AZ) boş ola bilməz.", nameof(companyAz));
        if (string.IsNullOrWhiteSpace(companyEn))
            throw new ArgumentException("Şirkət (EN) boş ola bilməz.", nameof(companyEn));
        if (string.IsNullOrWhiteSpace(companyRu))
            throw new ArgumentException("Şirkət (RU) boş ola bilməz.", nameof(companyRu));

        FullNameAz = fullNameAz;
        FullNameEn = fullNameEn;
        FullNameRu = fullNameRu;
        CompanyAz = companyAz;
        CompanyEn = companyEn;
        CompanyRu = companyRu;
    }

    public void Update(
        string fullNameAz, string fullNameEn, string fullNameRu,
        string companyAz, string companyEn, string companyRu)
    {
        if (string.IsNullOrWhiteSpace(fullNameAz))
            throw new ArgumentException("Tam ad (AZ) boş ola bilməz.", nameof(fullNameAz));
        if (string.IsNullOrWhiteSpace(fullNameEn))
            throw new ArgumentException("Tam ad (EN) boş ola bilməz.", nameof(fullNameEn));
        if (string.IsNullOrWhiteSpace(fullNameRu))
            throw new ArgumentException("Tam ad (RU) boş ola bilməz.", nameof(fullNameRu));
        if (string.IsNullOrWhiteSpace(companyAz))
            throw new ArgumentException("Şirkət (AZ) boş ola bilməz.", nameof(companyAz));
        if (string.IsNullOrWhiteSpace(companyEn))
            throw new ArgumentException("Şirkət (EN) boş ola bilməz.", nameof(companyEn));
        if (string.IsNullOrWhiteSpace(companyRu))
            throw new ArgumentException("Şirkət (RU) boş ola bilməz.", nameof(companyRu));

        FullNameAz = fullNameAz;
        FullNameEn = fullNameEn;
        FullNameRu = fullNameRu;
        CompanyAz = companyAz;
        CompanyEn = companyEn;
        CompanyRu = companyRu;
    }
}
