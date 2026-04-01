using App.BL.DTOs;

namespace App.BL.Mapper.ForeignRepresentatives;

public interface IForeignRepresentativesMapper
{
    Core.Entities.ForeignRepresentatives CreateDtoToDomain(CreateForeignRepresentativesDto dto);
    ForeignRepresentativesResponseDto DomainToResponseDto(Core.Entities.ForeignRepresentatives entity);
    Core.Entities.ForeignRepresentatives UpdateDtoToDomain(Core.Entities.ForeignRepresentatives entity, UpdateForeignRepresentativesDto dto);
}
