using App.BL.DTOs;
using App.Core.Entities.Common.Cloudinary;

namespace App.BL.Mapper.OurValues;

public interface IOurValuesMapper
{
    Core.Entities.OurValues CreateDtoToDomain(CreateOurValuesDto dto, CloudinaryURL imageUrl);
    OurValuesResponseDto DomainToResponseDto(Core.Entities.OurValues entity);
    Core.Entities.OurValues UpdateDtoToDomain(Core.Entities.OurValues entity, UpdateOurValuesDto dto, CloudinaryURL imageUrl);
}
