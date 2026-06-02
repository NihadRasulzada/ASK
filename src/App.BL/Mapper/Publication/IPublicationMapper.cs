using App.BL.DTOs;
using App.Core.Entities.Common.Storage;

namespace App.BL.Mapper.Publication;

public interface IPublicationMapper
{
    Core.Entities.Publication CreateDtoToDomain(CreatePublicationDto dto, StoredFile titleImageUrl, StoredFile pdfUrl);
    Core.Entities.Publication UpdateDtoToDomain(Core.Entities.Publication entity, UpdatePublicationDto dto, StoredFile? titleImageUrl = null, StoredFile? pdfUrl = null);
    PublicationResponseDto DomainToResponseDto(Core.Entities.Publication entity);
}
