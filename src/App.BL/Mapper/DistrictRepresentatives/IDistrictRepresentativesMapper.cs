using App.BL.DTOs;

namespace App.BL.Mapper.DistrictRepresentatives;

public interface IDistrictRepresentativesMapper
{
    Core.Entities.DistrictRepresentatives CreateDtoToDomain(CreateDistrictRepresentativesDto dto);
    DistrictRepresentativesResponseDto DomainToResponseDto(Core.Entities.DistrictRepresentatives entity);
    Core.Entities.DistrictRepresentatives UpdateDtoToDomain(Core.Entities.DistrictRepresentatives entity, UpdateDistrictRepresentativesDto dto);
}
