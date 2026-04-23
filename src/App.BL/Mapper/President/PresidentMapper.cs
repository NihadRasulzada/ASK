using App.BL.DTOs;
using App.BL.Services.External;
using App.Core.Entities.Common.Cloudinary;

namespace App.BL.Mapper.President;

public class PresidentMapper(IMediaUrlBuilder mediaUrlBuilder) : IPresidentMapper
{
    public Core.Entities.President CreateDtoToDomain(CreatePresidentDto dto, CloudinaryURL imageUrl)
    {
        return new Core.Entities.President(imageUrl, dto.Text);
    }

    public PresidentResponseDto DomainToResponseDto(Core.Entities.President entity)
    {
        return new PresidentResponseDto(entity.Id, mediaUrlBuilder.Build(entity.ImageUrl.ImageURl), entity.Text);
    }

    public Core.Entities.President UpdateDtoToDomain(Core.Entities.President entity, UpdatePresidentDto dto, CloudinaryURL? imageUrl = null)
    {
        entity.Update(dto.Text);
        if (imageUrl is not null)
            entity.UpdateImageUrl(imageUrl);
        return entity;
    }
}
