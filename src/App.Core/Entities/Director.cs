using App.Core.Entities.Common;
using App.Core.Entities.Common.Cloudinary;

namespace App.Core.Entities;

public class Director : SoftDeletableEntity
{
    public CloudinaryURL ImageUrl { get; private set; }
    public string FullNameAz { get; private set; }
    public string FullNameEn { get; private set; }
    public string FullNameRu { get; private set; }
    public string DutyAz { get; private set; }
    public string DutyEn { get; private set; }
    public string DutyRu { get; private set; }
    public string DepartmentAz { get; private set; }
    public string DepartmentEn { get; private set; }
    public string DepartmentRu { get; private set; }
    public string PhoneNumber { get; set; }
    public string  Email { get; set; }

    public Guid CloudinaryURLId { get; private set; }


    // EF Core materialization
    private Director() : base(Guid.Empty, false)
    {
    }

    public Director(
        CloudinaryURL imageUrl,
        string fullNameAz, string fullNameEn, string fullNameRu,
        string dutyAz, string dutyEn, string dutyRu,
        string departmentAz, string departmentEn, string departmentRu,
        string phoneNumber, string email)
        : base(Guid.NewGuid(), false)
    {
        if (CloudinaryURL.IsNullOrEmpty(imageUrl))
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
        DepartmentAz = departmentAz;
        DepartmentEn = departmentEn;
        DepartmentRu = departmentRu;
        PhoneNumber = phoneNumber;
        Email = email;
    }

    /// <param name="imageUrl">Null ötürülərsə mövcud dəyər saxlanılır.</param>
    public void Update(
        string fullNameAz, string fullNameEn, string fullNameRu,
        string dutyAz, string dutyEn, string dutyRu,
        string departmentAz, string departmentEn, string departmentRu,
        string phoneNumber, string email)
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
        DepartmentAz = departmentAz;
        DepartmentEn = departmentEn;
        DepartmentRu = departmentRu;
        PhoneNumber = phoneNumber;
        Email = email;
    }

    public void UpdateImageUrl(CloudinaryURL imageUrl)
    {
        if (CloudinaryURL.IsNullOrEmpty(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));

        ImageUrl = imageUrl;
    }
}