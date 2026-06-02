using App.BL.DTOs;
using App.BL.Services.External;
using App.Core.Entities.Common.Storage;
using App.Core.Interfaces;

namespace App.BL.Mapper.Director;

public class DirectorMapper : IDirectorMapper
{
    private readonly ILanguageService languageService;
    private readonly IMediaUrlBuilder mediaUrlBuilder;

    public DirectorMapper(ILanguageService languageService, IMediaUrlBuilder mediaUrlBuilder)
    {
        this.languageService = languageService;
        this.mediaUrlBuilder = mediaUrlBuilder;
    }

    public Core.Entities.Director CreateDtoToDomain(CreateDirectorDto dto, StoredFile imageUrl)
    {
        return new Core.Entities.Director(
            imageUrl,
            dto.FullNameAz,
            dto.FullNameEn,
            dto.FullNameRu,
            dto.DutyAz,
            dto.DutyEn,
            dto.DutyRu,
            dto.DepartmentAz ?? string.Empty,
            dto.DepartmentEn ?? string.Empty,
            dto.DepartmentRu ?? string.Empty,
            dto.PhoneNumber ?? string.Empty,
            dto.Email ?? string.Empty
        );
    }

    public DirectorResponseDto DomainToResponseDto(Core.Entities.Director Director)
    {
        return new DirectorResponseDto(
            Id: Director.Id,
            ImageUrl: mediaUrlBuilder.Build(Director.ImageUrl.ObjectKey),
            FullName: languageService.Lang switch
            {
                "az" => Director.FullNameAz,
                "en" => Director.FullNameEn,
                "ru" => Director.FullNameRu,
                _ => Director.FullNameAz
            },
            Duty: languageService.Lang switch
            {
                "az" => Director.DutyAz,
                "en" => Director.DutyEn,
                "ru" => Director.DutyRu,
                _ => Director.DutyAz
            },
            Department: languageService.Lang switch
            {
                "az" => string.IsNullOrWhiteSpace(Director.DepartmentAz) ? null : Director.DepartmentAz,
                "en" => string.IsNullOrWhiteSpace(Director.DepartmentEn) ? null : Director.DepartmentEn,
                "ru" => string.IsNullOrWhiteSpace(Director.DepartmentRu) ? null : Director.DepartmentRu,
                _ => string.IsNullOrWhiteSpace(Director.DepartmentAz) ? null : Director.DepartmentAz
            },
            PhoneNumber: string.IsNullOrWhiteSpace(Director.PhoneNumber) ? null : Director.PhoneNumber,
            Email: string.IsNullOrWhiteSpace(Director.Email) ? null : Director.Email,
            IsDeactive: Director.IsDeactive
        );
    }

    public Core.Entities.Director UpdateDtoToDamain(Core.Entities.Director Director, UpdateDirectorDto dto, StoredFile? imageUrl = null)
    {
        Director.Update(
            dto.FullNameAz,
            dto.FullNameEn,
            dto.FullNameRu,
            dto.DutyAz,
            dto.DutyEn,
            dto.DutyRu,
            dto.DepartmentAz ?? string.Empty,
            dto.DepartmentEn ?? string.Empty,
            dto.DepartmentRu ?? string.Empty,
            dto.PhoneNumber ?? string.Empty,
            dto.Email ?? string.Empty
        );
        if (imageUrl is not null)
            Director.UpdateImageUrl(imageUrl);

        return Director;
    }
}
