using App.BL.DTOs;
using App.Core.Entities.Common.Storage;

namespace App.BL.Mapper.Announcement;

public interface IAnnouncementMapper
{
    Core.Entities.Announcement CreateDtoToDomain(CreateAnnouncementDto dto, StoredFile titleImageUrl);
    Core.Entities.Announcement UpdateDtoToDomain(Core.Entities.Announcement entity, UpdateAnnouncementDto dto, StoredFile? titleImageUrl = null);
    AnnouncementResponseDto DomainToResponseDto(Core.Entities.Announcement entity);
}
