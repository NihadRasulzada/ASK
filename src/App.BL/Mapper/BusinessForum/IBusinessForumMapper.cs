using App.BL.DTOs;
using App.Core.Entities.Common.Cloudinary;

namespace App.BL.Mapper.BusinessForum;

public interface IBusinessForumMapper
{
    Core.Entities.BusinessForum CreateDtoToDomain(CreateBusinessForumDto dto, CloudinaryURL titleImageUrl, CloudinaryURL detailImageUrl);
    Core.Entities.BusinessForum UpdateDtoToDomain(Core.Entities.BusinessForum entity, UpdateBusinessForumDto dto, CloudinaryURL? titleImageUrl = null, CloudinaryURL? detailImageUrl = null);
    BusinessForumResponseDto DomainToResponseDto(Core.Entities.BusinessForum entity);
}
