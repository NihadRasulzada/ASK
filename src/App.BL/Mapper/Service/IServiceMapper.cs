using App.BL.DTOs;

namespace App.BL.Mapper.Service;

public interface IServiceMapper
{
    Core.Entities.Service CreateDtoToDomain(CreateServiceDto dto, string imageUrl);
    ServiceResponseDto DomainToResponseDto(Core.Entities.Service service);
    Core.Entities.Service UpdateDtoToDamain(Core.Entities.Service service, UpdateServiceDto dto, string imageUrl);
}