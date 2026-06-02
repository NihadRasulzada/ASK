using App.BL.DTOs;
using App.BL.Services.External;
using App.Core.Entities.Common.Storage;

namespace App.BL.Mapper.InternationalSolidarity;

public class InternationalSolidarityMapper(IMediaUrlBuilder mediaUrlBuilder) : IInternationalSolidarityMapper
{
    public Core.Entities.InternationalSolidarity CreateDtoToDomain(CreateInternationalSolidarityDto dto, StoredFile iconUrl)
    {
        return new Core.Entities.InternationalSolidarity(dto.Link, iconUrl);
    }

    public InternationalSolidarityResponseDto DomainToResponseDto(Core.Entities.InternationalSolidarity entity)
    {
        return new InternationalSolidarityResponseDto(
            Id: entity.Id,
            Link: entity.Link,
            IconUrl: mediaUrlBuilder.Build(entity.IconUrl.ObjectKey));
    }

    public Core.Entities.InternationalSolidarity UpdateDtoToDomain(Core.Entities.InternationalSolidarity entity, UpdateInternationalSolidarityDto dto, StoredFile iconUrl)
    {
        entity.Update(dto.Link);
        entity.UpdateIconUrl(iconUrl);
        return entity;
    }
}
