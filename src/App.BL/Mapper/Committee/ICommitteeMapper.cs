using App.BL.DTOs;

namespace App.BL.Mapper.Committee;

public interface ICommitteeMapper
{
    Core.Entities.Committee CreateDtoToDomain(CreateCommitteeDto dto);
    CommitteeResponseDto DomainToResponseDto(Core.Entities.Committee entity);
    Core.Entities.Committee UpdateDtoToDomain(Core.Entities.Committee entity, UpdateCommitteeDto dto);
}
