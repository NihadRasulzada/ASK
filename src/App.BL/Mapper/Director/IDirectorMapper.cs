using App.BL.DTOs;
using App.Core.Entities.Common.Cloudinary;

namespace App.BL.Mapper.Director;

public interface IDirectorMapper
{
    Core.Entities.Director CreateDtoToDomain(CreateDirectorDto dto, CloudinaryURL imageUrl);
    DirectorResponseDto DomainToResponseDto(Core.Entities.Director Director);
    Core.Entities.Director UpdateDtoToDamain(Core.Entities.Director Director, UpdateDirectorDto dto, CloudinaryURL imageUrl);
}