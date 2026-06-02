using App.BL.DTOs;
using App.BL.Services.External;
using App.Core.Entities.Common.Storage;

namespace App.BL.Mapper.Partner;

public class PartnerMapper(IMediaUrlBuilder mediaUrlBuilder) : IPartnerMapper
{
    public Core.Entities.Partner CreateDtoToDomain(CreatePartnerDto dto, StoredFile imageUrl)
    {
        return new Core.Entities.Partner(imageUrl, dto.Site);
    }

    public PartnerResponseDto DomainToResponseDto(Core.Entities.Partner entity)
    {
        return new PartnerResponseDto(entity.Id, mediaUrlBuilder.Build(entity.ImageUrl.ObjectKey), entity.Site);
    }

    public Core.Entities.Partner UpdateDtoToDomain(Core.Entities.Partner entity, UpdatePartnerDto dto, StoredFile? imageUrl = null)
    {
        entity.Update(dto.Site);
        if (imageUrl is not null)
            entity.UpdateImageUrl(imageUrl);
        return entity;
    }
}
