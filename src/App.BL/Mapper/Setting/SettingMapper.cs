using App.BL.DTOs;
using App.BL.Services.External;
using App.Core.Enums;

namespace App.BL.Mapper.Setting;

public class SettingMapper(IMediaUrlBuilder mediaUrlBuilder) : ISettingMapper
{
    public SettingResponseDto DomainToResponseDto(Core.Entities.Setting entity)
    {
        string? cloudinaryUrl = entity.MediaValue is not null
            ? mediaUrlBuilder.Build(entity.MediaValue.ObjectKey)
            : null;

        return new SettingResponseDto(
            Id: entity.Id,
            Key: entity.Key,
            StringValue: entity.ValueType == SettingValueType.Text
                ? mediaUrlBuilder.BuildHtml(entity.StringValue)
                : entity.StringValue,
            CloudinaryUrl: cloudinaryUrl,
            ValueType: entity.ValueType);
    }
}
