using App.BL.DTOs;

namespace App.BL.Mapper.Video;

public class VideoMapper : IVideoMapper
{
    public Core.Entities.Video CreateDtoToDomain(CreateVideoDto dto)
    {
        return new Core.Entities.Video(dto.Link, dto.Title);
    }

    public VideoResponseDto DomainToResponseDto(Core.Entities.Video entity)
    {
        return new VideoResponseDto(entity.Id, entity.Link, entity.Title);
    }

    public Core.Entities.Video UpdateDtoToDomain(Core.Entities.Video entity, UpdateVideoDto dto)
    {
        entity.Update(dto.Link, dto.Title);
        return entity;
    }
}
