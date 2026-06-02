using System;
using System.Collections.Generic;
using System.Text;
using App.BL.DTOs;
using App.Core.Entities.Common.Storage;

namespace App.BL.Mapper.NewsImage;

public interface INewsImageMapper
{
    Core.Entities.NewsImage CreateDtoToDomain(CreateNewsImageDto dto, StoredFile imageUrl);

    Core.Entities.NewsImage UpdateDtoToDomain(Core.Entities.NewsImage entity, UpdateNewsImageDto dto, StoredFile imageUrl);

    NewsImageResponseDto DomainToResponseDto(Core.Entities.NewsImage entity);
}
