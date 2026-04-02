using App.BL.DTOs;
using App.BL.Services.External;
using App.Core.Enums;

namespace App.BL.Mapper.Setting;

public class SettingMapper(IMediaUrlBuilder mediaUrlBuilder) : ISettingMapper
{
    public SettingResponseDto DomainToResponseDto(Core.Entities.Setting entity)
    {
        var value = entity.ValueType == SettingValueType.Link
            ? mediaUrlBuilder.Build(entity.Value)
            : entity.Value;

        return new SettingResponseDto(
            Id: entity.Id,
            Key: entity.Key,
            DisplayName: entity.DisplayName,
            Value: value,
            ValueType: entity.ValueType);
    }
}
