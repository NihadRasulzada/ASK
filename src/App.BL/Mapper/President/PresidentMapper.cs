using App.BL.DTOs;

namespace App.BL.Mapper.President;

public class PresidentMapper : IPresidentMapper
{
    public Core.Entities.President CreateDtoToDomain(CreatePresidentDto dto, string imageUrl)
    {
        return new Core.Entities.President(imageUrl, dto.Text);
    }

    public PresidentResponseDto DomainToResponseDto(Core.Entities.President entity)
    {
        return new PresidentResponseDto(entity.Id, entity.ImageUrl, entity.Text);
    }

    public Core.Entities.President UpdateDtoToDomain(Core.Entities.President entity, UpdatePresidentDto dto, string? imageUrl = null)
    {
        entity.Update(dto.Text);
        if(imageUrl is not null) 
            entity.UpdateImageUrl(imageUrl);
        return entity;
    }
}
