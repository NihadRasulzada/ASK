using App.BL.DTOs;

namespace App.BL.Mapper.News;

public interface INewsMapper
{
    Core.Entities.News CreateDtoToDomain(CreateNewsDto dto, string titleImageUrl, IList<string> imageUrls);

    void UpdateDtoToDomain(Core.Entities.News entity, UpdateNewsDto dto, string titleImageUrl, IList<string> imageUrls);

    NewsResponseDto DomainToResponseDto(Core.Entities.News entity);
}