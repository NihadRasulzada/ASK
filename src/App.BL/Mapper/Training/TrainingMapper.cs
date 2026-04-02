using App.BL.DTOs;
using App.BL.Services.External;

namespace App.BL.Mapper.Training;

public class TrainingMapper(IMediaUrlBuilder mediaUrlBuilder) : ITrainingMapper
{
    public Core.Entities.Training CreateDtoToDomain(CreateTrainingDto dto, string imageUrl)
    {
        return new Core.Entities.Training(dto.Title, imageUrl, dto.Text);
    }

    public TrainingResponseDto DomainToResponseDto(Core.Entities.Training entity)
    {
        return new TrainingResponseDto(entity.Id, entity.Title, entity.Text, mediaUrlBuilder.Build(entity.TitleImageUrl), entity.IsDeactive);
    }

    public Core.Entities.Training UpdateDtoToDomain(Core.Entities.Training entity, UpdateTrainingDto dto, string? imageUrl = null)
    {
        entity.Update(dto.Title, dto.Text);
        if (imageUrl != null)
        {
            entity.UpdateImageUrl(imageUrl);
        }
        return entity;
    }
}
