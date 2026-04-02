using App.BL.DTOs;
using App.BL.Services.External;
using App.Core.Interfaces;

namespace App.BL.Mapper.News;

public class NewsMapper : INewsMapper
{
    private readonly ILanguageService languageService;
    private readonly IMediaUrlBuilder mediaUrlBuilder;

    public NewsMapper(ILanguageService languageService, IMediaUrlBuilder mediaUrlBuilder)
    {
        this.languageService = languageService;
        this.mediaUrlBuilder = mediaUrlBuilder;
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
        return new NewsResponseDto(
            entity.Id,
            mediaUrlBuilder.Build(entity.TitleImageUrl),
            languageService.Lang switch
            {
                "az" => entity.NewsTextAz,
                "en" => entity.NewsTextEn,
                "ru" => entity.NewsTextRu,
                _ => entity.NewsTextAz
            },
            entity.Images?.Select(x => mediaUrlBuilder.Build(x.ImageUrl)!).ToList() ?? new List<string>(),
            entity.IsDeactive,
            entity.CreateDate
        );
    }

    public void UpdateDtoToDomain(Core.Entities.News entity, UpdateNewsDto dto, string titleImageUrl)
    {
        entity.Update(
            dto.NewsTextAz,
            dto.NewsTextEn,
            dto.NewsTextRu
        );
        if (titleImageUrl != null)
        {
            entity.UpdateImageUrl(titleImageUrl);
        }
    }
}
