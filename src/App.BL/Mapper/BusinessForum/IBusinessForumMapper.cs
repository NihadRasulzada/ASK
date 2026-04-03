using App.BL.DTOs;

namespace App.BL.Mapper.BusinessForum;

public interface IBusinessForumMapper
{
    Core.Entities.BusinessForum CreateDtoToDomain(CreateBusinessForumDto dto, string titleImageUrl, string detailImageUrl);
    Core.Entities.BusinessForum UpdateDtoToDomain(Core.Entities.BusinessForum entity, UpdateBusinessForumDto dto, string? titleImageUrl = null, string? detailImageUrl = null);
    BusinessForumResponseDto DomainToResponseDto(Core.Entities.BusinessForum entity);
}
