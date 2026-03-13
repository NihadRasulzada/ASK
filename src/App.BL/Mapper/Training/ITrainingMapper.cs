using App.BL.DTOs;

namespace App.BL.Mapper.Training;

public interface ITrainingMapper
{
    Core.Entities.Training CreateDtoToDomain(CreateTrainingDto dto, string imageUrl);
    Core.Entities.Training UpdateDtoToDomain(Core.Entities.Training entity, UpdateTrainingDto dto, string? imageUrl = null);
    TrainingResponseDto DomainToResponseDto(Core.Entities.Training entity);
}
