using App.BL.DTOs;
using App.Core.Interfaces;

namespace App.BL.Mapper.UsefulLink;

public class UsefulLinkMapper(ILanguageService languageService) : IUsefulLinkMapper
{
    public Core.Entities.UsefulLink CreateDtoToDomain(CreateUsefulLinkDto dto)
        => new(dto.TitleAz, dto.TitleEn, dto.TitleRu, dto.Link);

    public UsefulLinkResponseDto DomainToResponseDto(Core.Entities.UsefulLink entity)
        => new(
            Id: entity.Id,
            Title: languageService.Lang switch
            {
                "az" => entity.TitleAz,
                "en" => entity.TitleEn,
                "ru" => entity.TitleRu,
                _ => entity.TitleAz
            },
            Link: entity.Link,
            IsDeactive: entity.IsDeactive
        );

    public void UpdateDtoToDomain(Core.Entities.UsefulLink entity, UpdateUsefulLinkDto dto)
        => entity.Update(dto.TitleAz, dto.TitleEn, dto.TitleRu, dto.Link);
}
