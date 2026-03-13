using App.BL.DTOs;

namespace App.BL.Mapper.Partner;

public interface IPartnerMapper
{
    Core.Entities.Partner CreateDtoToDomain(CreatePartnerDto dto, string imageUrl);
    Core.Entities.Partner UpdateDtoToDomain(Core.Entities.Partner entity, UpdatePartnerDto dto, string? imageUrl = null);
    PartnerResponseDto DomainToResponseDto(Core.Entities.Partner entity);
}
