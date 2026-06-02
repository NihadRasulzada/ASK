using App.BL.DTOs;
using App.BL.Services.External;
using App.Core.Entities.Common.Storage;
using App.Core.Interfaces;

namespace App.BL.Mapper.Training;

public class TrainingMapper(ILanguageService languageService, IMediaUrlBuilder mediaUrlBuilder) : ITrainingMapper
{
    public Core.Entities.Training CreateDtoToDomain(CreateTrainingDto dto, StoredFile imageUrl)
    {
        return new Core.Entities.Training(dto.TitleAz, dto.TitleEn, dto.TitleRu, imageUrl, dto.TextAz, dto.TextEn, dto.TextRu, dto.StartDate, dto.EndDate);
    }

    public TrainingResponseDto DomainToResponseDto(Core.Entities.Training entity)
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


        return new TrainingResponseDto(entity.Id, title, text, mediaUrlBuilder.Build(entity.TitleImageUrl.ObjectKey), entity.IsDeactive, entity.Created);
    }
    public TrainingDateResponseDto DomainToResponseDateDto(Core.Entities.Training entity)
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


        return new TrainingDateResponseDto(entity.Id, title, text, mediaUrlBuilder.Build(entity.TitleImageUrl.ObjectKey), entity.IsDeactive, entity.Created, entity.StartDate, entity.EndDate);
    }

    public Core.Entities.Training UpdateDtoToDomain(Core.Entities.Training entity, UpdateTrainingDto dto, StoredFile? imageUrl = null)
    {
        entity.Update(dto.TitleAz, dto.TitleEn, dto.TitleRu, dto.TextAz, dto.TextEn, dto.TextRu, dto.StartDate, dto.EndDate);
        if (imageUrl != null)
        {
            entity.UpdateImageUrl(imageUrl);
        }
        return entity;
    }
}
