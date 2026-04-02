using App.BL.DTOs;
using App.BL.Services.External;
using App.Core.Interfaces;

namespace App.BL.Mapper.Service;

public class ServiceMapper : IServiceMapper
{
    private readonly ILanguageService _languageService;
    private readonly IMediaUrlBuilder _mediaUrlBuilder;

    public ServiceMapper(ILanguageService languageService, IMediaUrlBuilder mediaUrlBuilder)
    {
        _languageService = languageService;
        _mediaUrlBuilder = mediaUrlBuilder;
    }

    public Core.Entities.Service CreateDtoToDomain(CreateServiceDto dto, string imageUrl)
    {
        return new Core.Entities.Service(
            imageUrl: imageUrl,
            nameAz: dto.NameAz,
            nameEn: dto.NameEn,
            nameRu: dto.NameRu
        );
    }

    public ServiceResponseDto DomainToResponseDto(Core.Entities.Service service)
    {
        return new ServiceResponseDto(
            Id: service.Id,
            ImageUrl: _mediaUrlBuilder.Build(service.ImageUrl),
            Name: _languageService.Lang switch
            {
                "az" => service.NameAz,
                "en" => service.NameEn,
                "ru" => service.NameRu,
                _ => throw new NotSupportedException($"Language {_languageService.Lang} is not supported.")
            },
            IsDeactive: service.IsDeactive
        );
    }

    public Core.Entities.Service UpdateDtoToDamain(Core.Entities.Service service, UpdateServiceDto dto, string imageUrl)
    {
        service.Update(
            nameAz: dto.NameAz,
            nameEn: dto.NameEn,
            nameRu: dto.NameRu
        );
        if (imageUrl is not null)
        {
            service.UpdateImageUrl(imageUrl);
        }
        return service;
    }
}
