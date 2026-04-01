using App.BL.DTOs;

namespace App.BL.Mapper.OurValues;

public interface IOurValuesMapper
{
    Core.Entities.OurValues CreateDtoToDomain(CreateOurValuesDto dto, string imageUrl);
    OurValuesResponseDto DomainToResponseDto(Core.Entities.OurValues entity);
    Core.Entities.OurValues UpdateDtoToDomain(Core.Entities.OurValues entity, UpdateOurValuesDto dto, string imageUrl);
}
