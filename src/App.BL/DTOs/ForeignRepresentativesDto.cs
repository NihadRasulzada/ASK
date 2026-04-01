namespace App.BL.DTOs;

public record CreateForeignRepresentativesDto(
    string CountryAz, string CountryEn, string CountryRu,
    string FullNameAz, string FullNameEn, string FullNameRu,
    string CompanyAz, string CompanyEn, string CompanyRu,
    string DutyAz, string DutyEn, string DutyRu);

public record UpdateForeignRepresentativesDto(
    string CountryAz, string CountryEn, string CountryRu,
    string FullNameAz, string FullNameEn, string FullNameRu,
    string CompanyAz, string CompanyEn, string CompanyRu,
    string DutyAz, string DutyEn, string DutyRu);

public record ForeignRepresentativesResponseDto(
    Guid Id,
    string Country,
    string FullName,
    string Company,
    string Duty);
