using App.BL.DTOs;
using App.Core.Entities.Common.Storage;

namespace App.BL.Mapper.Service;

public interface IServiceMapper
{
    Core.Entities.Service CreateDtoToDomain(CreateServiceDto dto, StoredFile imageUrl);
    ServiceResponseDto DomainToResponseDto(Core.Entities.Service service);
    Core.Entities.Service UpdateDtoToDamain(Core.Entities.Service service, UpdateServiceDto dto, StoredFile imageUrl);
}