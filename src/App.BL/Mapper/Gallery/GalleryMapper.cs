using App.BL.DTOs;
using App.BL.Services.External;
using App.Core.Entities.Common.Storage;

namespace App.BL.Mapper.Gallery;

public class GalleryMapper(IMediaUrlBuilder mediaUrlBuilder) : IGalleryMapper
{
    public Core.Entities.Gallery CreateDtoToDomain(CreateGalleryDto dto, StoredFile imageUrl)
    {
        return new Core.Entities.Gallery(imageUrl);
    }

    public GalleryResponseDto DomainToResponseDto(Core.Entities.Gallery entity)
    {
        return new GalleryResponseDto(entity.Id, mediaUrlBuilder.Build(entity.ImageUrl.ObjectKey));
    }

    public Core.Entities.Gallery UpdateDtoToDomain(Core.Entities.Gallery entity, UpdateGalleryDto dto, StoredFile imageUrl)
    {
        entity.UpdateImageUrl(imageUrl);
        return entity;
    }
}
