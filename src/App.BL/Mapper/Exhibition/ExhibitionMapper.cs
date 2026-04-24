using App.BL.DTOs;
using App.BL.Services.External;
using App.Core.Entities.Common.Cloudinary;

namespace App.BL.Mapper.Exhibition;

public class ExhibitionMapper(IMediaUrlBuilder mediaUrlBuilder) : IExhibitionMapper
{
    public Core.Entities.Exhibition CreateDtoToDomain(CreateExhibitionDto dto, CloudinaryURL imageUrl)
    {
        return new Core.Entities.Exhibition(dto.Title, imageUrl, dto.Text);
    }

    public ExhibitionResponseDto DomainToResponseDto(Core.Entities.Exhibition entity)
    {
        return new ExhibitionResponseDto(entity.Id, entity.Title, entity.Text, mediaUrlBuilder.Build(entity.TitleImageUrl.ImageURl), entity.IsDeactive, entity.Created);
    }

    public Core.Entities.Exhibition UpdateDtoToDomain(Core.Entities.Exhibition entity, UpdateExhibitionDto dto, CloudinaryURL? imageUrl = null)
    {
        entity.Update(dto.Title, dto.Text);
        if (imageUrl != null)
        {
            entity.UpdateImageUrl(imageUrl);
        }
        return entity;
    }
}
