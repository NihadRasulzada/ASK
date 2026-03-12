using App.BL.DTOs;

namespace App.BL.Mapper.Director;

public interface IDirectorMapper
{
    Core.Entities.Director CreateDtoToDomain(CreateDirectorDto dto, string imageUrl);
    DirectorResponseDto DomainToResponseDto(Core.Entities.Director Director);
    Core.Entities.Director UpdateDtoToDamain(Core.Entities.Director Director, UpdateDirectorDto dto, string imageUrl);
}