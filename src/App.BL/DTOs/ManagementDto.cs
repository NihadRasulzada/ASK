namespace App.BL.DTOs;

public record CreateManagementDto(
    string FullNameAz, string FullNameEn, string FullNameRu,
    string CompanyAz, string CompanyEn, string CompanyRu);

public record UpdateManagementDto(
    string FullNameAz, string FullNameEn, string FullNameRu,
    string CompanyAz, string CompanyEn, string CompanyRu);

public record ManagementResponseDto(
    Guid Id,
    string FullName,
    string Company);
