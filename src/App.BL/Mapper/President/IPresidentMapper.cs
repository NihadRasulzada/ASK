using App.BL.DTOs;

namespace App.BL.Mapper.President;

public interface IPresidentMapper
{
    Core.Entities.President CreateDtoToDomain(CreatePresidentDto dto, string imageUrl);
    Core.Entities.President UpdateDtoToDomain(Core.Entities.President entity, UpdatePresidentDto dto, string? imageUrl = null);
    PresidentResponseDto DomainToResponseDto(Core.Entities.President entity);
}
