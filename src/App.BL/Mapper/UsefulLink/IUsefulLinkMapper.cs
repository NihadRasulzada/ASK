using App.BL.DTOs;

namespace App.BL.Mapper.UsefulLink;

public interface IUsefulLinkMapper
{
    Core.Entities.UsefulLink CreateDtoToDomain(CreateUsefulLinkDto dto);
    UsefulLinkResponseDto DomainToResponseDto(Core.Entities.UsefulLink entity);
    void UpdateDtoToDomain(Core.Entities.UsefulLink entity, UpdateUsefulLinkDto dto);
}
