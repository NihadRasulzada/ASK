using App.BL.DTOs;
using App.Core.Interfaces;

namespace App.BL.Mapper.DistrictRepresentatives;

public class DistrictRepresentativesMapper(ILanguageService languageService) : IDistrictRepresentativesMapper
{
    public Core.Entities.DistrictRepresentatives CreateDtoToDomain(CreateDistrictRepresentativesDto dto)
    {
        return new Core.Entities.DistrictRepresentatives(
            dto.DistrictAz, dto.DistrictEn, dto.DistrictRu,
            dto.FullNameAz, dto.FullNameEn, dto.FullNameRu,
            dto.CompanyAz, dto.CompanyEn, dto.CompanyRu);
    }

    public DistrictRepresentativesResponseDto DomainToResponseDto(Core.Entities.DistrictRepresentatives entity)
    {
        return new DistrictRepresentativesResponseDto(
            Id: entity.Id,
            District: languageService.Lang switch
            {
                "az" => entity.DistrictAz,
                "en" => entity.DistrictEn,
                "ru" => entity.DistrictRu,
                _ => entity.DistrictAz
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
            });
    }

    public Core.Entities.DistrictRepresentatives UpdateDtoToDomain(Core.Entities.DistrictRepresentatives entity, UpdateDistrictRepresentativesDto dto)
    {
        entity.Update(
            dto.DistrictAz, dto.DistrictEn, dto.DistrictRu,
            dto.FullNameAz, dto.FullNameEn, dto.FullNameRu,
            dto.CompanyAz, dto.CompanyEn, dto.CompanyRu);
        return entity;
    }
}
