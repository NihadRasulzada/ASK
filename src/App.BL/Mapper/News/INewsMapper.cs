using App.BL.DTOs;
using App.Core.Entities.Common.Cloudinary;

namespace App.BL.Mapper.News;

public interface INewsMapper
{
    Core.Entities.News CreateDtoToDomain(CreateNewsDto dto, CloudinaryURL titleImageUrl);

    void UpdateDtoToDomain(Core.Entities.News entity, UpdateNewsDto dto, CloudinaryURL titleImageUrl);

    NewsResponseDto DomainToResponseDto(Core.Entities.News entity);
}