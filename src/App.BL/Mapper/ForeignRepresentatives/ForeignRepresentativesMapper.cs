using App.BL.DTOs;
using App.Core.Interfaces;

namespace App.BL.Mapper.ForeignRepresentatives;

public class ForeignRepresentativesMapper(ILanguageService languageService) : IForeignRepresentativesMapper
{
    public Core.Entities.ForeignRepresentatives CreateDtoToDomain(CreateForeignRepresentativesDto dto)
    {
        return new Core.Entities.ForeignRepresentatives(
            dto.CountryAz, dto.CountryEn, dto.CountryRu,
            dto.FullNameAz, dto.FullNameEn, dto.FullNameRu,
            dto.CompanyAz, dto.CompanyEn, dto.CompanyRu,
            dto.DutyAz, dto.DutyEn, dto.DutyRu);
    }

    public ForeignRepresentativesResponseDto DomainToResponseDto(Core.Entities.ForeignRepresentatives entity)
    {
        return new ForeignRepresentativesResponseDto(
            Id: entity.Id,
            Country: languageService.Lang switch
            {
                "az" => entity.CountryAz,
                "en" => entity.CountryEn,
                "ru" => entity.CountryRu,
                _ => entity.CountryAz
            },
            FullName: languageService.Lang switch
            {
                "az" => entity.FullNameAz,
                "en" => entity.FullNameEn,
                "ru" => entity.FullNameRu,
                _ => entity.FullNameAz
            },
            Company: languageService.Lang switch
            {
                "az" => entity.CompanyAz,
                "en" => entity.CompanyEn,
                "ru" => entity.CompanyRu,
                _ => entity.CompanyAz
            },
            Duty: languageService.Lang switch
            {
                "az" => entity.DutyAz,
                "en" => entity.DutyEn,
                "ru" => entity.DutyRu,
                _ => entity.DutyAz
            });
    }

    public Core.Entities.ForeignRepresentatives UpdateDtoToDomain(Core.Entities.ForeignRepresentatives entity, UpdateForeignRepresentativesDto dto)
    {
        entity.Update(
            dto.CountryAz, dto.CountryEn, dto.CountryRu,
            dto.FullNameAz, dto.FullNameEn, dto.FullNameRu,
            dto.CompanyAz, dto.CompanyEn, dto.CompanyRu,
            dto.DutyAz, dto.DutyEn, dto.DutyRu);
        return entity;
    }
}
