using App.BL.DTOs;
using App.Core.Entities.Common.Cloudinary;

namespace App.BL.Mapper.Presidium;

public interface IPresidiumMapper
{
    Core.Entities.Presidium CreateDtoToDomain(CreatePresidiumDto dto, CloudinaryURL imageUrl);
    PresidiumResponseDto DomainToResponseDto(Core.Entities.Presidium entity);
    Core.Entities.Presidium UpdateDtoToDomain(Core.Entities.Presidium entity, UpdatePresidiumDto dto, CloudinaryURL imageUrl);
}
