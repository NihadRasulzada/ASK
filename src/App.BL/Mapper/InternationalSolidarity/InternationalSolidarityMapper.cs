using App.BL.DTOs;

namespace App.BL.Mapper.InternationalSolidarity;

public class InternationalSolidarityMapper : IInternationalSolidarityMapper
{
    public Core.Entities.InternationalSolidarity CreateDtoToDomain(CreateInternationalSolidarityDto dto, string iconUrl)
    {
        return new Core.Entities.InternationalSolidarity(dto.Link, iconUrl);
    }

    public InternationalSolidarityResponseDto DomainToResponseDto(Core.Entities.InternationalSolidarity entity)
    {
        return new InternationalSolidarityResponseDto(
            Id: entity.Id,
            Link: entity.Link,
            IconUrl: entity.IconUrl);
    }

    public Core.Entities.InternationalSolidarity UpdateDtoToDomain(Core.Entities.InternationalSolidarity entity, UpdateInternationalSolidarityDto dto, string iconUrl)
    {
        entity.Update(dto.Link);
        entity.UpdateIconUrl(iconUrl);
        return entity;
    }
}
