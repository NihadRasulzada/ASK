using App.BL.DTOs;
using App.Core.Interfaces;

namespace App.BL.Mapper.Committee;

public class CommitteeMapper(ILanguageService languageService) : ICommitteeMapper
{
    public Core.Entities.Committee CreateDtoToDomain(CreateCommitteeDto dto)
    {
        return new Core.Entities.Committee(
            dto.NameAz, dto.NameEn, dto.NameRu,
            dto.ChairmanAz, dto.ChairmanEn, dto.ChairmanRu,
            dto.VicePresidentAz, dto.VicePresidentEn, dto.VicePresidentRu);
    }

    public CommitteeResponseDto DomainToResponseDto(Core.Entities.Committee entity)
    {
        return new CommitteeResponseDto(
            Id: entity.Id,
            Name: languageService.Lang switch
            {
                "az" => entity.NameAz,
                "en" => entity.NameEn,
                "ru" => entity.NameRu,
                _ => entity.NameAz
            },
            Chairman: languageService.Lang switch
            {
                "az" => entity.ChairmanAz,
                "en" => entity.ChairmanEn,
                "ru" => entity.ChairmanRu,
                _ => entity.ChairmanAz
            },
            VicePresident: languageService.Lang switch
            {
                "az" => entity.VicePresidentAz,
                "en" => entity.VicePresidentEn,
                "ru" => entity.VicePresidentRu,
                _ => entity.VicePresidentAz
            });
    }

    public Core.Entities.Committee UpdateDtoToDomain(Core.Entities.Committee entity, UpdateCommitteeDto dto)
    {
        entity.Update(
            dto.NameAz, dto.NameEn, dto.NameRu,
            dto.ChairmanAz, dto.ChairmanEn, dto.ChairmanRu,
            dto.VicePresidentAz, dto.VicePresidentEn, dto.VicePresidentRu);
        return entity;
    }
}
