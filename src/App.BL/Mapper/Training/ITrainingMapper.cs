using App.BL.DTOs;
using App.Core.Entities.Common.Cloudinary;

namespace App.BL.Mapper.Training;

public interface ITrainingMapper
{
    Core.Entities.Training CreateDtoToDomain(CreateTrainingDto dto, CloudinaryURL imageUrl);
    Core.Entities.Training UpdateDtoToDomain(Core.Entities.Training entity, UpdateTrainingDto dto, CloudinaryURL? imageUrl = null);
    TrainingResponseDto DomainToResponseDto(Core.Entities.Training entity);
    TrainingDateResponseDto DomainToResponseDateDto(Core.Entities.Training entity);
}
