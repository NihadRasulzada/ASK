using App.BL.DTOs;

namespace App.BL.Mapper.Gallery;

public interface IGalleryMapper
{
    Core.Entities.Gallery CreateDtoToDomain(CreateGalleryDto dto, string imageUrl);
    Core.Entities.Gallery UpdateDtoToDomain(Core.Entities.Gallery entity, UpdateGalleryDto dto, string? imageUrl = null);
    GalleryResponseDto DomainToResponseDto(Core.Entities.Gallery entity);
}
