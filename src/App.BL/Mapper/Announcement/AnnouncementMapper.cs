using App.BL.DTOs;
using App.BL.Services.External;
using App.Core.Entities.Common.Cloudinary;

namespace App.BL.Mapper.Announcement;

public class AnnouncementMapper(IMediaUrlBuilder mediaUrlBuilder) : IAnnouncementMapper
{
    public Core.Entities.Announcement CreateDtoToDomain(CreateAnnouncementDto dto, CloudinaryURL titleImageUrl)
    {
        return new Core.Entities.Announcement(dto.Title, titleImageUrl, dto.Text);
    }

    public AnnouncementResponseDto DomainToResponseDto(Core.Entities.Announcement entity)
    {
        return new AnnouncementResponseDto(entity.Id, entity.Title, mediaUrlBuilder.Build(entity.TitleImageUrl.ImageURl), entity.Text);
    }

    public Core.Entities.Announcement UpdateDtoToDomain(Core.Entities.Announcement entity, UpdateAnnouncementDto dto, CloudinaryURL? titleImageUrl = null)
    {
        entity.Update(dto.Title, dto.Text);
        if (titleImageUrl is not null)
            entity.UpdateImageUrl(titleImageUrl);
        return entity;
    }
}
