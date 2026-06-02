using App.BL.DTOs;
using App.BL.Services.External;
using App.Core.Entities.Common.Storage;
using App.Core.Interfaces;

namespace App.BL.Mapper.OurValues;

public class OurValuesMapper(ILanguageService languageService, IMediaUrlBuilder mediaUrlBuilder) : IOurValuesMapper
{
    public Core.Entities.OurValues CreateDtoToDomain(CreateOurValuesDto dto, StoredFile imageUrl)
    {
        return new Core.Entities.OurValues(
            dto.TitleAz, dto.TitleEn, dto.TitleRu,
            imageUrl);
    }

    public OurValuesResponseDto DomainToResponseDto(Core.Entities.OurValues entity)
    {
        return new OurValuesResponseDto(
            Id: entity.Id,
            Title: languageService.Lang switch
            {
                "az" => entity.TitleAz,
                "en" => entity.TitleEn,
                "ru" => entity.TitleRu,
                _ => entity.TitleAz
            },
            ImageUrl: mediaUrlBuilder.Build(entity.ImageUrl.ObjectKey));
    }

    public Core.Entities.OurValues UpdateDtoToDomain(Core.Entities.OurValues entity, UpdateOurValuesDto dto, StoredFile imageUrl)
    {
        entity.Update(dto.TitleAz, dto.TitleEn, dto.TitleRu);
        entity.UpdateImageUrl(imageUrl);
        return entity;
    }
}
