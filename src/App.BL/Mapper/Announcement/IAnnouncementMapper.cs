using App.BL.DTOs;

namespace App.BL.Mapper.Announcement;

public interface IAnnouncementMapper
{
    Core.Entities.Announcement CreateDtoToDomain(CreateAnnouncementDto dto, string titleImageUrl);
    Core.Entities.Announcement UpdateDtoToDomain(Core.Entities.Announcement entity, UpdateAnnouncementDto dto, string? titleImageUrl = null);
    AnnouncementResponseDto DomainToResponseDto(Core.Entities.Announcement entity);
}
