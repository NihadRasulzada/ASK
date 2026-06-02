using App.BL.DTOs;
using App.Core.Entities.Common.Storage;

namespace App.BL.Mapper.Gallery;

public interface IGalleryMapper
{
    Core.Entities.Gallery CreateDtoToDomain(CreateGalleryDto dto, StoredFile imageUrl);
    Core.Entities.Gallery UpdateDtoToDomain(Core.Entities.Gallery entity, UpdateGalleryDto dto, StoredFile? imageUrl = null);
    GalleryResponseDto DomainToResponseDto(Core.Entities.Gallery entity);
}
