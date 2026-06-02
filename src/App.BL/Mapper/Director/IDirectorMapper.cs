using App.BL.DTOs;
using App.Core.Entities.Common.Storage;

namespace App.BL.Mapper.Director;

public interface IDirectorMapper
{
    Core.Entities.Director CreateDtoToDomain(CreateDirectorDto dto, StoredFile imageUrl);
    DirectorResponseDto DomainToResponseDto(Core.Entities.Director Director);
    Core.Entities.Director UpdateDtoToDamain(Core.Entities.Director Director, UpdateDirectorDto dto, StoredFile imageUrl);
}