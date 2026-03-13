using App.BL.DTOs;

namespace App.BL.Mapper.Exhibition;

public interface IExhibitionMapper
{
    Core.Entities.Exhibition CreateDtoToDomain(CreateExhibitionDto dto, string imageUrl);
    Core.Entities.Exhibition UpdateDtoToDomain(Core.Entities.Exhibition entity, UpdateExhibitionDto dto, string? imageUrl = null);
    ExhibitionResponseDto DomainToResponseDto(Core.Entities.Exhibition entity);
}
