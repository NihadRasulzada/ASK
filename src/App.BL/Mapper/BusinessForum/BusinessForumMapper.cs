using App.BL.DTOs;
using App.BL.Services.External;
using App.Core.Interfaces;

namespace App.BL.Mapper.BusinessForum;

public class BusinessForumMapper(ILanguageService languageService, IMediaUrlBuilder mediaUrlBuilder) : IBusinessForumMapper
{
    public Core.Entities.BusinessForum CreateDtoToDomain(CreateBusinessForumDto dto, string titleImageUrl, string detailImageUrl)
    {
        return new Core.Entities.BusinessForum(
            titleImageUrl,
            dto.TitleAz, dto.TitleEn, dto.TitleRu,
            dto.TextAz, dto.TextEn, dto.TextRu,
            detailImageUrl);
    }

    public BusinessForumResponseDto DomainToResponseDto(Core.Entities.BusinessForum entity)
    {
        var title = languageService.Lang switch
        {
            "az" => entity.TitleAz,
            "en" => entity.TitleEn,
            "ru" => entity.TitleRu,
            _ => entity.TitleAz
        };

        var text = languageService.Lang switch
        {
            "az" => entity.TextAz,
            "en" => entity.TextEn,
            "ru" => entity.TextRu,
            _ => entity.TextAz
        };

        return new BusinessForumResponseDto(
            Id: entity.Id,
            Title: title,
            Text: text,
            TitleImageUrl: mediaUrlBuilder.Build(entity.TitleImageUrl)!,
            DetailImageUrl: mediaUrlBuilder.Build(entity.DetailImageUrl)!,
            CreateDate: entity.CreateDate);
    }

    public Core.Entities.BusinessForum UpdateDtoToDomain(Core.Entities.BusinessForum entity, UpdateBusinessForumDto dto, string? titleImageUrl = null, string? detailImageUrl = null)
    {
        entity.Update(dto.TitleAz, dto.TitleEn, dto.TitleRu, dto.TextAz, dto.TextEn, dto.TextRu);
        if (titleImageUrl is not null) entity.UpdateTitleImage(titleImageUrl);
        if (detailImageUrl is not null) entity.UpdateDetailImage(detailImageUrl);
        return entity;
    }
}
