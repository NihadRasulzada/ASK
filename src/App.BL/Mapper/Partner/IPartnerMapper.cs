using App.BL.DTOs;
using App.Core.Entities.Common.Storage;

namespace App.BL.Mapper.Partner;

public interface IPartnerMapper
{
    Core.Entities.Partner CreateDtoToDomain(CreatePartnerDto dto, StoredFile imageUrl);
    Core.Entities.Partner UpdateDtoToDomain(Core.Entities.Partner entity, UpdatePartnerDto dto, StoredFile? imageUrl = null);
    PartnerResponseDto DomainToResponseDto(Core.Entities.Partner entity);
}
