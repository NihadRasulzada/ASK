using App.BL.DTOs;
using App.BL.Services.External;

namespace App.BL.Mapper.Setting;

public class SettingMapper(IMediaUrlBuilder mediaUrlBuilder) : ISettingMapper
{
    public SettingResponseDto DomainToResponseDto(Core.Entities.Setting entity)
    {
        string? mediaUrl = entity.MediaValue is not null
            ? mediaUrlBuilder.Build(entity.MediaValue.ObjectKey)
            : null;

        return new SettingResponseDto(
            Id: entity.Id,
            Key: entity.Key,
            StringValue: entity.StringValue,
            MediaUrl: mediaUrl,
            ValueType: entity.ValueType);
    }
}
