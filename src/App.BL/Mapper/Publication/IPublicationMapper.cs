using App.BL.DTOs;

namespace App.BL.Mapper.Publication;

public interface IPublicationMapper
{
    Core.Entities.Publication CreateDtoToDomain(CreatePublicationDto dto, string titleImageUrl, string pdfUrl);
    Core.Entities.Publication UpdateDtoToDomain(Core.Entities.Publication entity, UpdatePublicationDto dto, string? titleImageUrl = null, string? pdfUrl = null);
    PublicationResponseDto DomainToResponseDto(Core.Entities.Publication entity);
}
