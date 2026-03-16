using App.BL.DTOs;

namespace App.BL.Mapper.Partner;

public class PartnerMapper : IPartnerMapper
{
    public Core.Entities.Partner CreateDtoToDomain(CreatePartnerDto dto, string imageUrl)
    {
        return new Core.Entities.Partner(imageUrl, dto.Site);
    }

    public PartnerResponseDto DomainToResponseDto(Core.Entities.Partner entity)
    {
        return new PartnerResponseDto(entity.Id, entity.ImageUrl, entity.Site);
    }

    public Core.Entities.Partner UpdateDtoToDomain(Core.Entities.Partner entity, UpdatePartnerDto dto, string? imageUrl = null)
    {
        entity.Update(dto.Site);
        if(imageUrl is not null) 
            entity.UpdateImageUrl(imageUrl);
        return entity;
    }
}
