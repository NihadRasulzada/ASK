using App.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.BL.Mapper.NewsImage;

public interface INewsImageMapper
{
    Core.Entities.NewsImage CreateDtoToDomain(CreateNewsImageDto dto, string imageUrl);

    Core.Entities.NewsImage UpdateDtoToDomain(Core.Entities.NewsImage entity, UpdateNewsImageDto dto, string imageUrl);

    NewsImageResponseDto DomainToResponseDto(Core.Entities.NewsImage entity);
}
