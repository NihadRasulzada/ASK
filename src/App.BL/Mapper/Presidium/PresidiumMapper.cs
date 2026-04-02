using App.BL.DTOs;
using App.BL.Services.External;
using App.Core.Interfaces;

namespace App.BL.Mapper.Presidium;

public class PresidiumMapper(ILanguageService languageService, IMediaUrlBuilder mediaUrlBuilder) : IPresidiumMapper
{
    public Core.Entities.Presidium CreateDtoToDomain(CreatePresidiumDto dto, string imageUrl)
    {
        return new Core.Entities.Presidium(
            dto.FullNameAz, dto.FullNameEn, dto.FullNameRu,
            dto.PositionAz, dto.PositionEn, dto.PositionRu,
            imageUrl);
    }

    public PresidiumResponseDto DomainToResponseDto(Core.Entities.Presidium entity)
    {
        return new PresidiumResponseDto(
            Id: entity.Id,
            FullName: languageService.Lang switch
            {
                "az" => entity.FullNameAz,
                "en" => entity.FullNameEn,
                "ru" => entity.FullNameRu,
                _ => entity.FullNameAz
            },
            Position: languageService.Lang switch
            {
                "az" => entity.PositionAz,
                "en" => entity.PositionEn,
                "ru" => entity.PositionRu,
                _ => entity.PositionAz
            },
            ImageUrl: mediaUrlBuilder.Build(entity.ImageUrl));
    }

    public Core.Entities.Presidium UpdateDtoToDomain(Core.Entities.Presidium entity, UpdatePresidiumDto dto, string imageUrl)
    {
        entity.Update(
            dto.FullNameAz, dto.FullNameEn, dto.FullNameRu,
            dto.PositionAz, dto.PositionEn, dto.PositionRu);
        entity.UpdateImageUrl(imageUrl);
        return entity;
    }
}
