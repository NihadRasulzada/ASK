using App.BL.DTOs;
using App.Core.Entities.Common.Cloudinary;

namespace App.BL.Mapper.Gallery;

public interface IGalleryMapper
{
    Core.Entities.Gallery CreateDtoToDomain(CreateGalleryDto dto, CloudinaryURL imageUrl);
    Core.Entities.Gallery UpdateDtoToDomain(Core.Entities.Gallery entity, UpdateGalleryDto dto, CloudinaryURL? imageUrl = null);
    GalleryResponseDto DomainToResponseDto(Core.Entities.Gallery entity);
}
