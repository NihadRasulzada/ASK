using App.BL.DTOs;
using App.Core.Entities.Common.Storage;

namespace App.BL.Mapper.Exhibition;

public interface IExhibitionMapper
{
    Core.Entities.Exhibition CreateDtoToDomain(CreateExhibitionDto dto, StoredFile imageUrl);
    Core.Entities.Exhibition UpdateDtoToDomain(Core.Entities.Exhibition entity, UpdateExhibitionDto dto, StoredFile? imageUrl = null);
    ExhibitionResponseDto DomainToResponseDto(Core.Entities.Exhibition entity);
    ExhibitionDateResponseDto DomainToResponseDateDto(Core.Entities.Exhibition entity);
}
