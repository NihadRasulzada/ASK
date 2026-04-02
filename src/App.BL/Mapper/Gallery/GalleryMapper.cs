using App.BL.DTOs;
using App.BL.Services.External;

namespace App.BL.Mapper.Gallery;

public class GalleryMapper(IMediaUrlBuilder mediaUrlBuilder) : IGalleryMapper
{
    public Core.Entities.Gallery CreateDtoToDomain(CreateGalleryDto dto, string imageUrl)
    {
        return new Core.Entities.Gallery(imageUrl);
    }

    public GalleryResponseDto DomainToResponseDto(Core.Entities.Gallery entity)
    {
        return new GalleryResponseDto(entity.Id, mediaUrlBuilder.Build(entity.ImageUrl));
    }

    public Core.Entities.Gallery UpdateDtoToDomain(Core.Entities.Gallery entity, UpdateGalleryDto dto, string imageUrl)
    {
        entity.Update(imageUrl);
        return entity;
    }
}
