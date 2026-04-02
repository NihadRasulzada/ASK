using App.BL.DTOs;

namespace App.BL.Mapper.Setting;

public class SettingMapper : ISettingMapper
{
    public SettingResponseDto DomainToResponseDto(Core.Entities.Setting entity)
    {
        return new SettingResponseDto(
            Id: entity.Id,
            Key: entity.Key,
            DisplayName: entity.DisplayName,
            Value: entity.Value,
            ValueType: entity.ValueType);
    }
}
