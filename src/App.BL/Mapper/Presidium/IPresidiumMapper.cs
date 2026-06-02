using App.BL.DTOs;
using App.Core.Entities.Common.Storage;

namespace App.BL.Mapper.Presidium;

public interface IPresidiumMapper
{
    Core.Entities.Presidium CreateDtoToDomain(CreatePresidiumDto dto, StoredFile imageUrl);
    PresidiumResponseDto DomainToResponseDto(Core.Entities.Presidium entity);
    Core.Entities.Presidium UpdateDtoToDomain(Core.Entities.Presidium entity, UpdatePresidiumDto dto, StoredFile imageUrl);
}
