using App.BL.DTOs;

namespace App.BL.Mapper.Video;

public class VideoMapper : IVideoMapper
{
    public Core.Entities.Video CreateDtoToDomain(CreateVideoDto dto)
    {
        return new Core.Entities.Video(dto.Link);
    }

    public VideoResponseDto DomainToResponseDto(Core.Entities.Video entity)
    {
        return new VideoResponseDto(entity.Id, entity.Link);
    }

    public Core.Entities.Video UpdateDtoToDomain(Core.Entities.Video entity, UpdateVideoDto dto)
    {
        entity.Update(dto.Link);
        return entity;
    }
}
