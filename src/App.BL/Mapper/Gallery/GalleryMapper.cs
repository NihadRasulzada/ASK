using App.BL.DTOs;

namespace App.BL.Mapper.Gallery;

public class GalleryMapper : IGalleryMapper
{
    public Core.Entities.Gallery CreateDtoToDomain(CreateGalleryDto dto, string imageUrl)
    {
        return new Core.Entities.Gallery
        {
            ImageUrl = imageUrl
        };
    }

    public GalleryResponseDto DomainToResponseDto(Core.Entities.Gallery entity)
    {
        return new GalleryResponseDto(entity.Id, entity.ImageUrl);
    }

    public Core.Entities.Gallery UpdateDtoToDomain(Core.Entities.Gallery entity, UpdateGalleryDto dto, string? imageUrl = null)
    {
        if (!string.IsNullOrWhiteSpace(imageUrl))
        {
            entity.ImageUrl = imageUrl;
        }

        return entity;
    }
}
