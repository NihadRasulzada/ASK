using App.BL.DTOs;

namespace App.BL.Mapper.Training;

public class TrainingMapper : ITrainingMapper
{
    public Core.Entities.Training CreateDtoToDomain(CreateTrainingDto dto, string imageUrl)
    {
        return new Core.Entities.Training
        {
            Title = dto.Title,
            Text = dto.Text,
            TitleImageUrl = imageUrl
        };
    }

    public TrainingResponseDto DomainToResponseDto(Core.Entities.Training entity)
    {
        return new TrainingResponseDto(entity.Id, entity.Title, entity.Text, entity.TitleImageUrl, entity.IsDeactive);
    }

    public Core.Entities.Training UpdateDtoToDomain(Core.Entities.Training entity, UpdateTrainingDto dto, string? imageUrl = null)
    {
        entity.Title = dto.Title;
        entity.Text = dto.Text;
        if (imageUrl != null)
        {
            entity.TitleImageUrl = imageUrl;
        }
        return entity;
    }
}
