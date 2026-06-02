using App.BL.DTOs;
using App.BL.Services.External;
using App.Core.Entities.Common.Storage;
using App.Core.Interfaces;

namespace App.BL.Mapper.Publication;

public class PublicationMapper(ILanguageService languageService, IMediaUrlBuilder mediaUrlBuilder) : IPublicationMapper
{
    public Core.Entities.Publication CreateDtoToDomain(CreatePublicationDto dto, StoredFile titleImageUrl, StoredFile pdfUrl)
    {
        return new Core.Entities.Publication(
            titleImageUrl,
            dto.TitleAz,
            dto.TitleEn,
            dto.TitleRu,
            pdfUrl);
    }

    public PublicationResponseDto DomainToResponseDto(Core.Entities.Publication entity)
    {
        var title = languageService.Lang switch
        {
            "az" => entity.TitleAz,
            "en" => entity.TitleEn,
            "ru" => entity.TitleRu,
            _ => entity.TitleAz
        };

        return new PublicationResponseDto(
            Id: entity.Id,
            Title: title,
            TitleImageUrl: mediaUrlBuilder.Build(entity.TitleImageUrl.ObjectKey)!,
            PdfUrl: mediaUrlBuilder.Build(entity.PdfUrl.ObjectKey)!);
    }

    public Core.Entities.Publication UpdateDtoToDomain(Core.Entities.Publication entity, UpdatePublicationDto dto, StoredFile? titleImageUrl = null, StoredFile? pdfUrl = null)
    {
        entity.Update(dto.TitleAz, dto.TitleEn, dto.TitleRu);
        if (titleImageUrl is not null) entity.UpdateTitleImage(titleImageUrl);
        if (pdfUrl is not null) entity.UpdatePdf(pdfUrl);
        return entity;
    }
}
