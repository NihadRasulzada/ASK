using App.BL.DTOs;
using App.Core.Interfaces;
using FluentValidation.Resources;

namespace App.BL.Mapper.News;

public class NewsMapper : INewsMapper
{
    private readonly ILanguageService languageService;

    public NewsMapper(ILanguageService languageService)
    {
        this.languageService = languageService;
    }

    public Core.Entities.News CreateDtoToDomain(CreateNewsDto dto, string titleImageUrl)
    {
        var entity = new Core.Entities.News(
            titleImageUrl,
            dto.NewsTextAz,
            dto.NewsTextEn,
            dto.NewsTextRu
        );

        return entity;
    }

    public NewsResponseDto DomainToResponseDto(Core.Entities.News entity)
    {
        // App.Core.Entities.News doesn't have a NewsText property directly, but NewsResponseDto expects string NewsText
        // Since NewsResponseDto has NewsText instead of Az/En/Ru, I will map it to Az by default or combine them if needed
        // For now, mapping NewsTextAz to NewsText
        return new NewsResponseDto(
            entity.Id,
            entity.TitleImageUrl,
            languageService.Lang switch
            {
                "az" => entity.NewsTextAz,
                "en" => entity.NewsTextEn,
                "ru" => entity.NewsTextRu,
                _ => entity.NewsTextAz
            },
            entity.Images?.Select(x => x.ImageUrl).ToList() ?? new List<string>(),
            entity.IsDeactive
        );
    }

    public void UpdateDtoToDomain(Core.Entities.News entity, UpdateNewsDto dto, string titleImageUrl)
    {
        entity.Update(
            dto.NewsTextAz,
            dto.NewsTextEn,
            dto.NewsTextRu
        );
    }
}