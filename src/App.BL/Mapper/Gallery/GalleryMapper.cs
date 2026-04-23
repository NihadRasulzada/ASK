using App.BL.DTOs;
using App.BL.Services.External;
using App.Core.Entities.Common.Cloudinary;

namespace App.BL.Mapper.Gallery;

public class GalleryMapper(IMediaUrlBuilder mediaUrlBuilder) : IGalleryMapper
{
    public Core.Entities.Gallery CreateDtoToDomain(CreateGalleryDto dto, CloudinaryURL imageUrl)
    {
        return new Core.Entities.Gallery(imageUrl);
    }

    public GalleryResponseDto DomainToResponseDto(Core.Entities.Gallery entity)
    {
        return new GalleryResponseDto(entity.Id, mediaUrlBuilder.Build(entity.ImageUrl.ImageURl));
    }

    public Core.Entities.Gallery UpdateDtoToDomain(Core.Entities.Gallery entity, UpdateGalleryDto dto, CloudinaryURL imageUrl)
    {
        entity.UpdateImageUrl(imageUrl);
        return entity;
    }
}
