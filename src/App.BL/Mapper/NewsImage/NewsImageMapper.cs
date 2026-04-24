using App.BL.DTOs;
using App.BL.Services.External;
using App.Core.Entities.Common.Cloudinary;

namespace App.BL.Mapper.NewsImage;

public class NewsImageMapper(IMediaUrlBuilder mediaUrlBuilder) : INewsImageMapper
{
    public Core.Entities.NewsImage CreateDtoToDomain(CreateNewsImageDto dto, CloudinaryURL imageUrl)
    {
        return new Core.Entities.NewsImage(
            imageUrl,
            dto.NewsId
        );
    }

    public NewsImageResponseDto DomainToResponseDto(Core.Entities.NewsImage entity)
    {
        return new NewsImageResponseDto(entity.Id, mediaUrlBuilder.Build(entity.ImageUrl.ImageURl), entity.NewsId);
    }

    public Core.Entities.NewsImage UpdateDtoToDomain(Core.Entities.NewsImage entity, UpdateNewsImageDto dto, CloudinaryURL imageUrl)
    {
        entity.UpdateImageUrl(imageUrl);
        return entity;
    }
}
