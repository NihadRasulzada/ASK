using App.BL.DTOs;
using App.BL.Services.External;
using App.Core.Entities.Common.Storage;
using App.Core.Interfaces;

namespace App.BL.Mapper.Announcement;

public class AnnouncementMapper : IAnnouncementMapper
{
    private readonly ILanguageService languageService;
    private readonly IMediaUrlBuilder mediaUrlBuilder;

    public AnnouncementMapper(ILanguageService languageService, IMediaUrlBuilder mediaUrlBuilder)
    {
        this.languageService = languageService;
        this.mediaUrlBuilder = mediaUrlBuilder;
    }
    public Core.Entities.Announcement CreateDtoToDomain(CreateAnnouncementDto dto, StoredFile titleImageUrl)
    {
        return new Core.Entities.Announcement(
            dto.TitleAz, 
            dto.TitleEn, 
            dto.TitleRu, 
            titleImageUrl, 
            dto.TextAz, 
            dto.TextEn, 
            dto.TextRu
        );
    }

    public AnnouncementResponseDto DomainToResponseDto(Core.Entities.Announcement entity)
    {
        //return new AnnouncementResponseDto(entity.Id, entity.TitleAz, mediaUrlBuilder.Build(entity.TitleImageUrl.ObjectKey), entity.TextAz, entity.Created);
        return new AnnouncementResponseDto(
            Id: entity.Id,
            TitleImageUrl: mediaUrlBuilder.Build(entity.TitleImageUrl.ObjectKey),
            Title:languageService.Lang switch
            {
                "az" => entity.TitleAz,
                "en" => entity.TitleEn,
                "ru" => entity.TitleRu,
                _ => throw new NotImplementedException($"Language {languageService.Lang} is not implemented")
            },
            Text: languageService.Lang switch
            {
                "az" => entity.TextAz,
                "en" => entity.TextEn,
                "ru" => entity.TextRu,
                _ => throw new NotImplementedException($"Language {languageService.Lang} is not implemented")
            },
            Created: entity.Created
        );
    }

    public Core.Entities.Announcement UpdateDtoToDomain(Core.Entities.Announcement entity, UpdateAnnouncementDto dto, StoredFile? titleImageUrl = null)
    {
        entity.Update(
            dto.TitleAz, 
            dto.TitleEn, 
            dto.TitleRu, 
            dto.TextAz, 
            dto.TextEn, 
            dto.TextRu);
        if (titleImageUrl is not null)
            entity.UpdateImageUrl(titleImageUrl);
        return entity;
    }
}
