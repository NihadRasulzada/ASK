using App.BL.DTOs;

namespace App.BL.Mapper.Video;

public interface IVideoMapper
{
    Core.Entities.Video CreateDtoToDomain(CreateVideoDto dto);

    Core.Entities.Video UpdateDtoToDomain(Core.Entities.Video entity, UpdateVideoDto dto);

    VideoResponseDto DomainToResponseDto(Core.Entities.Video entity);
}
