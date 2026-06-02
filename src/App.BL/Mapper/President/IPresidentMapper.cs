using App.BL.DTOs;
using App.Core.Entities.Common.Storage;

namespace App.BL.Mapper.President;

public interface IPresidentMapper
{
    Core.Entities.President CreateDtoToDomain(CreatePresidentDto dto, StoredFile imageUrl);
    Core.Entities.President UpdateDtoToDomain(Core.Entities.President entity, UpdatePresidentDto dto, StoredFile? imageUrl = null);
    PresidentResponseDto DomainToResponseDto(Core.Entities.President entity);
}
