using App.BL.DTOs;

namespace App.BL.Mapper.Presidium;

public interface IPresidiumMapper
{
    Core.Entities.Presidium CreateDtoToDomain(CreatePresidiumDto dto, string imageUrl);
    PresidiumResponseDto DomainToResponseDto(Core.Entities.Presidium entity);
    Core.Entities.Presidium UpdateDtoToDomain(Core.Entities.Presidium entity, UpdatePresidiumDto dto, string imageUrl);
}
