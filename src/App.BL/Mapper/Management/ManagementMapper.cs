using App.BL.DTOs;
using App.Core.Interfaces;

namespace App.BL.Mapper.Management;

public class ManagementMapper(ILanguageService languageService) : IManagementMapper
{
    public Core.Entities.Management CreateDtoToDomain(CreateManagementDto dto)
    {
        return new Core.Entities.Management(
            dto.FullNameAz, dto.FullNameEn, dto.FullNameRu,
            dto.CompanyAz, dto.CompanyEn, dto.CompanyRu);
    }

    public ManagementResponseDto DomainToResponseDto(Core.Entities.Management entity)
    {
        return new ManagementResponseDto(
            Id: entity.Id,
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

    public Core.Entities.Management UpdateDtoToDomain(Core.Entities.Management entity, UpdateManagementDto dto)
    {
        entity.Update(
            dto.FullNameAz, dto.FullNameEn, dto.FullNameRu,
            dto.CompanyAz, dto.CompanyEn, dto.CompanyRu);
        return entity;
    }
}
