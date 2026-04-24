using System;
using System.Collections.Generic;
using System.Text;
using App.BL.DTOs;
using App.Core.Entities.Common.Cloudinary;

namespace App.BL.Mapper.NewsImage;

public interface INewsImageMapper
{
    Core.Entities.NewsImage CreateDtoToDomain(CreateNewsImageDto dto, CloudinaryURL imageUrl);

    Core.Entities.NewsImage UpdateDtoToDomain(Core.Entities.NewsImage entity, UpdateNewsImageDto dto, CloudinaryURL imageUrl);

    NewsImageResponseDto DomainToResponseDto(Core.Entities.NewsImage entity);
}
