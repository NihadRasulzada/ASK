using App.BL.DTOs;

namespace App.BL.Mapper.InternationalSolidarity;

public interface IInternationalSolidarityMapper
{
    Core.Entities.InternationalSolidarity CreateDtoToDomain(CreateInternationalSolidarityDto dto, string iconUrl);
    InternationalSolidarityResponseDto DomainToResponseDto(Core.Entities.InternationalSolidarity entity);
    Core.Entities.InternationalSolidarity UpdateDtoToDomain(Core.Entities.InternationalSolidarity entity, UpdateInternationalSolidarityDto dto, string iconUrl);
}
