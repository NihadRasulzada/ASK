using App.BL.DTOs;
using App.Core.Entities.Common.Cloudinary;

namespace App.BL.Mapper.Publication;

public interface IPublicationMapper
{
    Core.Entities.Publication CreateDtoToDomain(CreatePublicationDto dto, CloudinaryURL titleImageUrl, CloudinaryURL pdfUrl);
    Core.Entities.Publication UpdateDtoToDomain(Core.Entities.Publication entity, UpdatePublicationDto dto, CloudinaryURL? titleImageUrl = null, CloudinaryURL? pdfUrl = null);
    PublicationResponseDto DomainToResponseDto(Core.Entities.Publication entity);
}
