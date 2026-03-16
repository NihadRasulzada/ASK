using App.BL.DTOs;

namespace App.BL.Mapper.Exhibition;

public class ExhibitionMapper : IExhibitionMapper
{
    public Core.Entities.Exhibition CreateDtoToDomain(CreateExhibitionDto dto, string imageUrl)
    {
        return new Core.Entities.Exhibition(dto.Title, imageUrl, dto.Text);
    }

    public ExhibitionResponseDto DomainToResponseDto(Core.Entities.Exhibition entity)
    {
        return new ExhibitionResponseDto(entity.Id, entity.Title, entity.Text, entity.TitleImageUrl, entity.IsDeactive);
    }

    public Core.Entities.Exhibition UpdateDtoToDomain(Core.Entities.Exhibition entity, UpdateExhibitionDto dto, string? imageUrl = null)
    {
        entity.Update(dto.Title, dto.Text);
        if (imageUrl != null)
        {
            entity.UpdateImageUrl(imageUrl);
        }
        return entity;
    }
}
