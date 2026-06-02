using App.BL.DTOs;
using App.Core.Entities.Common.Storage;

namespace App.BL.Mapper.News;

public interface INewsMapper
{
    Core.Entities.News CreateDtoToDomain(CreateNewsDto dto, StoredFile titleImageUrl);

    void UpdateDtoToDomain(Core.Entities.News entity, UpdateNewsDto dto, StoredFile titleImageUrl);

    NewsResponseDto DomainToResponseDto(Core.Entities.News entity);
}