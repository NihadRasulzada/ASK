using App.BL.DTOs;
using App.BL.Services.External;
using App.Core.Enums;

namespace App.BL.Mapper.Setting;

public class SettingMapper(IMediaUrlBuilder mediaUrlBuilder) : ISettingMapper
{
    public SettingResponseDto DomainToResponseDto(Core.Entities.Setting entity)
    {
        return new SettingResponseDto(
            Id: entity.Id,
            Key: entity.Key,
            Value: entity.Value,
            ValueType: entity.ValueType);
    }
}
