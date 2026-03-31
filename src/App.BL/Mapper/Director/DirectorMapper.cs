using App.BL.DTOs;
using App.Core.Interfaces;

namespace App.BL.Mapper.Director;

public class DirectorMapper : IDirectorMapper
{
    private readonly ILanguageService languageService;

    public DirectorMapper(ILanguageService languageService)
    {
        this.languageService = languageService;
    }

    public Core.Entities.Director CreateDtoToDomain(CreateDirectorDto dto, string imageUrl)
    {
        return new Core.Entities.Director(
            imageUrl,
            dto.FullNameAz,
            dto.FullNameEn,
            dto.FullNameRu,
            dto.DutyAz,
            dto.DutyEn,
            dto.DutyRu,
            dto.PhoneNumber ?? string.Empty,
            dto.Email ?? string.Empty
        );
    }

    public DirectorResponseDto DomainToResponseDto(Core.Entities.Director Director)
    {
        return new DirectorResponseDto(
            Id: Director.Id,
            ImageUrl: Director.ImageUrl,
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
            PhoneNumber: string.IsNullOrWhiteSpace(Director.PhoneNumber) ? null : Director.PhoneNumber,
            Email: string.IsNullOrWhiteSpace(Director.Email) ? null : Director.Email,
            IsDeactive: Director.IsDeactive
        );
    }

    public Core.Entities.Director UpdateDtoToDamain(Core.Entities.Director Director, UpdateDirectorDto dto, string? imageUrl = null)
    {
        Director.Update(
            dto.FullNameAz,
            dto.FullNameEn,
            dto.FullNameRu,
            dto.DutyAz,
            dto.DutyEn,
            dto.DutyRu,
            dto.PhoneNumber ?? string.Empty,
            dto.Email ?? string.Empty
        );
        if (imageUrl is not null)
            Director.UpdateImageUrl(imageUrl);

        return Director;
    }
}