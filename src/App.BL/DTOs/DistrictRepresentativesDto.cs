namespace App.BL.DTOs;

public record CreateDistrictRepresentativesDto(
    string DistrictAz, string DistrictEn, string DistrictRu,
    string FullNameAz, string FullNameEn, string FullNameRu,
    string CompanyAz, string CompanyEn, string CompanyRu);

public record UpdateDistrictRepresentativesDto(
    string DistrictAz, string DistrictEn, string DistrictRu,
    string FullNameAz, string FullNameEn, string FullNameRu,
    string CompanyAz, string CompanyEn, string CompanyRu);

public record DistrictRepresentativesResponseDto(
    Guid Id,
    string District,
    string FullName,
    string Company);
