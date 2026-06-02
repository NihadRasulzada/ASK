using App.BL.DTOs;
using App.Core.Entities.Common.Storage;

namespace App.BL.Mapper.InternationalSolidarity;

public interface IInternationalSolidarityMapper
{
    Core.Entities.InternationalSolidarity CreateDtoToDomain(CreateInternationalSolidarityDto dto, StoredFile iconUrl);
    InternationalSolidarityResponseDto DomainToResponseDto(Core.Entities.InternationalSolidarity entity);
    Core.Entities.InternationalSolidarity UpdateDtoToDomain(Core.Entities.InternationalSolidarity entity, UpdateInternationalSolidarityDto dto, StoredFile iconUrl);
}
