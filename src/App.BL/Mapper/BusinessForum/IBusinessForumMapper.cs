using App.BL.DTOs;
using App.Core.Entities.Common.Storage;

namespace App.BL.Mapper.BusinessForum;

public interface IBusinessForumMapper
{
    Core.Entities.BusinessForum CreateDtoToDomain(CreateBusinessForumDto dto, StoredFile titleImageUrl, StoredFile detailImageUrl);
    Core.Entities.BusinessForum UpdateDtoToDomain(Core.Entities.BusinessForum entity, UpdateBusinessForumDto dto, StoredFile? titleImageUrl = null, StoredFile? detailImageUrl = null);
    BusinessForumResponseDto DomainToResponseDto(Core.Entities.BusinessForum entity);
}
