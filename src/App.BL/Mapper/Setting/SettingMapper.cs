using App.BL.DTOs;
using App.BL.Services.External;
using App.Core.Enums;

namespace App.BL.Mapper.Setting;

public class SettingMapper(IMediaUrlBuilder mediaUrlBuilder) : ISettingMapper
{
    public SettingResponseDto DomainToResponseDto(Core.Entities.Setting entity)
    {
        string? cloudinaryUrl = entity.CloudinaryValue is not null
            ? mediaUrlBuilder.Build(entity.CloudinaryValue.ImageURl)
            : null;

        return new SettingResponseDto(
            Id: entity.Id,
            Key: entity.Key,
            StringValue: entity.StringValue,
            CloudinaryUrl: cloudinaryUrl,
            ValueType: entity.ValueType);
    }
}
