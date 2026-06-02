using App.BL.DTOs;
using App.Core.Entities.Common.Storage;

namespace App.BL.Mapper.OurValues;

public interface IOurValuesMapper
{
    Core.Entities.OurValues CreateDtoToDomain(CreateOurValuesDto dto, StoredFile imageUrl);
    OurValuesResponseDto DomainToResponseDto(Core.Entities.OurValues entity);
    Core.Entities.OurValues UpdateDtoToDomain(Core.Entities.OurValues entity, UpdateOurValuesDto dto, StoredFile imageUrl);
}
