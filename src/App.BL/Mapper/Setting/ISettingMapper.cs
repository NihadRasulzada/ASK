using App.BL.DTOs;

namespace App.BL.Mapper.Setting;

public interface ISettingMapper
{
    SettingResponseDto DomainToResponseDto(Core.Entities.Setting entity);
}
