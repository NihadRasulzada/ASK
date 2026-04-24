using App.BL.DTOs;
using App.Core.Entities.Common.Cloudinary;

namespace App.BL.Mapper.President;

public interface IPresidentMapper
{
    Core.Entities.President CreateDtoToDomain(CreatePresidentDto dto, CloudinaryURL imageUrl);
    Core.Entities.President UpdateDtoToDomain(Core.Entities.President entity, UpdatePresidentDto dto, CloudinaryURL? imageUrl = null);
    PresidentResponseDto DomainToResponseDto(Core.Entities.President entity);
}
