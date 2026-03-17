using App.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.BL.Mapper.NewsImage;

public class NewsImageMapper : INewsImageMapper
{
    public Core.Entities.NewsImage CreateDtoToDomain(CreateNewsImageDto dto, string imageUrl)
    {
        return new Core.Entities.NewsImage(
            imageUrl,
            dto.NewsId
        );
    }

    public NewsImageResponseDto DomainToResponseDto(Core.Entities.NewsImage entity)
    {
        return new NewsImageResponseDto(entity.Id, entity.ImageUrl, entity.NewsId);
    }

    public Core.Entities.NewsImage UpdateDtoToDomain(Core.Entities.NewsImage entity, UpdateNewsImageDto dto, string imageUrl)
    {
        entity.Update(imageUrl);
        return entity;
    }
}
