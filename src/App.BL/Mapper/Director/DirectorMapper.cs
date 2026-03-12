using App.BL.DTOs;
using App.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.BL.Mapper.Director;

internal class DirectorMapper : IDirectorMapper
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
            dto.DutyRu
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
            IsDeactive: Director.IsDeactive
        );
    }

    public Core.Entities.Director UpdateDtoToDamain(Core.Entities.Director Director, UpdateDirectorDto dto, string imageUrl)
    {
        Director.Update(
            imageUrl,
            dto.FullNameAz,
            dto.FullNameEn,
            dto.FullNameRu,
            dto.DutyAz,
            dto.DutyEn,
            dto.DutyRu
        );
        return Director;
    }
}