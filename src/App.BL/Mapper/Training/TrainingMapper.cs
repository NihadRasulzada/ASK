using App.BL.DTOs;
using App.BL.Services.External;
using App.Core.Entities.Common.Cloudinary;

namespace App.BL.Mapper.Training;

public class TrainingMapper(IMediaUrlBuilder mediaUrlBuilder) : ITrainingMapper
{
    public Core.Entities.Training CreateDtoToDomain(CreateTrainingDto dto, CloudinaryURL imageUrl)
    {
        return new Core.Entities.Training(dto.Title, imageUrl, dto.Text);
    }

    public TrainingResponseDto DomainToResponseDto(Core.Entities.Training entity)
    {
        return new TrainingResponseDto(entity.Id, entity.Title, entity.Text, mediaUrlBuilder.Build(entity.TitleImageUrl.ImageURl), entity.IsDeactive, entity.Created);
    }

    public Core.Entities.Training UpdateDtoToDomain(Core.Entities.Training entity, UpdateTrainingDto dto, CloudinaryURL? imageUrl = null)
    {
        entity.Update(dto.Title, dto.Text);
        if (imageUrl != null)
        {
            entity.UpdateImageUrl(imageUrl);
        }
        return entity;
    }
}
