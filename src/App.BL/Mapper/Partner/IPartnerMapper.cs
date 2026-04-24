using App.BL.DTOs;
using App.Core.Entities.Common.Cloudinary;

namespace App.BL.Mapper.Partner;

public interface IPartnerMapper
{
    Core.Entities.Partner CreateDtoToDomain(CreatePartnerDto dto, CloudinaryURL imageUrl);
    Core.Entities.Partner UpdateDtoToDomain(Core.Entities.Partner entity, UpdatePartnerDto dto, CloudinaryURL? imageUrl = null);
    PartnerResponseDto DomainToResponseDto(Core.Entities.Partner entity);
}
