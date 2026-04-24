using App.BL.DTOs;
using App.Core.Entities.Common.Cloudinary;

namespace App.BL.Mapper.InternationalSolidarity;

public interface IInternationalSolidarityMapper
{
    Core.Entities.InternationalSolidarity CreateDtoToDomain(CreateInternationalSolidarityDto dto, CloudinaryURL iconUrl);
    InternationalSolidarityResponseDto DomainToResponseDto(Core.Entities.InternationalSolidarity entity);
    Core.Entities.InternationalSolidarity UpdateDtoToDomain(Core.Entities.InternationalSolidarity entity, UpdateInternationalSolidarityDto dto, CloudinaryURL iconUrl);
}
