using App.BL.DTOs;
using App.BL.Services.External;
using App.Core.Entities.Common.Cloudinary;
using App.Core.Interfaces;

namespace App.BL.Mapper.BusinessForum;

public class BusinessForumMapper(ILanguageService languageService, IMediaUrlBuilder mediaUrlBuilder) : IBusinessForumMapper
{
    public Core.Entities.BusinessForum CreateDtoToDomain(CreateBusinessForumDto dto, CloudinaryURL titleImageUrl, CloudinaryURL detailImageUrl)
    {
        return new Core.Entities.BusinessForum(
            dto.TitleAz, dto.TitleEn, dto.TitleRu,
            titleImageUrl,
            dto.TextAz, dto.TextEn, dto.TextRu,
            dto.StartDate, dto.EndDate, detailImageUrl);
    }

    public BusinessForumDateResponseDto DomainToDateResponseDto(Core.Entities.BusinessForum entity)
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

        return new BusinessForumDateResponseDto(
            Id: entity.Id,
            Title: title,
            Text: text,
            TitleImageUrl: mediaUrlBuilder.Build(entity.TitleImageUrl.ImageURl)!,
            DetailImageUrl: mediaUrlBuilder.Build(entity.DetailImageUrl.ImageURl)!,
            CreateDate: entity.Created,
            StartDate: entity.StartDate,
            EndDate: entity.EndDate);
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
            TitleImageUrl: mediaUrlBuilder.Build(entity.TitleImageUrl.ImageURl)!,
            DetailImageUrl: mediaUrlBuilder.Build(entity.DetailImageUrl.ImageURl)!,
            CreateDate: entity.Created);
    }

    public Core.Entities.BusinessForum UpdateDtoToDomain(Core.Entities.BusinessForum entity, UpdateBusinessForumDto dto, CloudinaryURL? titleImageUrl = null, CloudinaryURL? detailImageUrl = null)
    {
        entity.Update(dto.TitleAz, dto.TitleEn, dto.TitleRu, dto.TextAz, dto.TextEn, dto.TextRu, dto.StartDate, dto.EndDate);
        if (titleImageUrl is not null) entity.UpdateImageUrl(titleImageUrl);
        if (detailImageUrl is not null) entity.UpdateDetailImageUrl(detailImageUrl);
        return entity;
    }
}
