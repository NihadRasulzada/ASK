using App.BL.DTOs;

namespace App.BL.Mapper.Management;

public interface IManagementMapper
{
    Core.Entities.Management CreateDtoToDomain(CreateManagementDto dto);
    ManagementResponseDto DomainToResponseDto(Core.Entities.Management entity);
    Core.Entities.Management UpdateDtoToDomain(Core.Entities.Management entity, UpdateManagementDto dto);
}
