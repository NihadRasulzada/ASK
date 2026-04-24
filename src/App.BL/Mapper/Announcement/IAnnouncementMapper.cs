using App.BL.DTOs;
using App.Core.Entities.Common.Cloudinary;

namespace App.BL.Mapper.Announcement;

public interface IAnnouncementMapper
{
    Core.Entities.Announcement CreateDtoToDomain(CreateAnnouncementDto dto, CloudinaryURL titleImageUrl);
    Core.Entities.Announcement UpdateDtoToDomain(Core.Entities.Announcement entity, UpdateAnnouncementDto dto, CloudinaryURL? titleImageUrl = null);
    AnnouncementResponseDto DomainToResponseDto(Core.Entities.Announcement entity);
}
