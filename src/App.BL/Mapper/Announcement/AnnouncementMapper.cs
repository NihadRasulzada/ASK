using App.BL.DTOs;

namespace App.BL.Mapper.Announcement;

public class AnnouncementMapper : IAnnouncementMapper
{
    public Core.Entities.Announcement CreateDtoToDomain(CreateAnnouncementDto dto, string titleImageUrl)
    {
        return new Core.Entities.Announcement(dto.Title, titleImageUrl, dto.Text);
    }

    public AnnouncementResponseDto DomainToResponseDto(Core.Entities.Announcement entity)
    {
        return new AnnouncementResponseDto(entity.Id, entity.Title, entity.TitleImageUrl, entity.Text);
    }

    public Core.Entities.Announcement UpdateDtoToDomain(Core.Entities.Announcement entity, UpdateAnnouncementDto dto, string? titleImageUrl = null)
    {
        entity.Update(dto.Title, dto.Text);
        if (titleImageUrl is not null) 
            entity.UpdateImage(titleImageUrl);
        return entity;
    }
}
