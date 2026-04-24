using App.BL.DTOs;
using App.Core.Entities.Common.Cloudinary;

namespace App.BL.Mapper.Exhibition;

public interface IExhibitionMapper
{
    Core.Entities.Exhibition CreateDtoToDomain(CreateExhibitionDto dto, CloudinaryURL imageUrl);
    Core.Entities.Exhibition UpdateDtoToDomain(Core.Entities.Exhibition entity, UpdateExhibitionDto dto, CloudinaryURL? imageUrl = null);
    ExhibitionResponseDto DomainToResponseDto(Core.Entities.Exhibition entity);
}
