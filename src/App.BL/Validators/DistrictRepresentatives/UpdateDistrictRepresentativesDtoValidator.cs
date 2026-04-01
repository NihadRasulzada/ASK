using App.BL.DTOs;
using App.BL.Resources;
using App.Core.Interfaces;
using FluentValidation;

namespace App.BL.Validators.DistrictRepresentatives;

public class UpdateDistrictRepresentativesDtoValidator : AbstractValidator<UpdateDistrictRepresentativesDto>
{
    public UpdateDistrictRepresentativesDtoValidator(ILanguageService languageService)
    {
        RuleFor(x => x.DistrictAz).NotEmpty().WithMessage(ValidationMessages.DistrictRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.DistrictTooLong(languageService.Lang));
        RuleFor(x => x.DistrictEn).NotEmpty().WithMessage(ValidationMessages.DistrictRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.DistrictTooLong(languageService.Lang));
        RuleFor(x => x.DistrictRu).NotEmpty().WithMessage(ValidationMessages.DistrictRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.DistrictTooLong(languageService.Lang));

        RuleFor(x => x.FullNameAz).NotEmpty().WithMessage(ValidationMessages.FullNameRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.FullNameTooLong(languageService.Lang));
        RuleFor(x => x.FullNameEn).NotEmpty().WithMessage(ValidationMessages.FullNameRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.FullNameTooLong(languageService.Lang));
        RuleFor(x => x.FullNameRu).NotEmpty().WithMessage(ValidationMessages.FullNameRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.FullNameTooLong(languageService.Lang));

        RuleFor(x => x.CompanyAz).NotEmpty().WithMessage(ValidationMessages.CompanyRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.CompanyTooLong(languageService.Lang));
        RuleFor(x => x.CompanyEn).NotEmpty().WithMessage(ValidationMessages.CompanyRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.CompanyTooLong(languageService.Lang));
        RuleFor(x => x.CompanyRu).NotEmpty().WithMessage(ValidationMessages.CompanyRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.CompanyTooLong(languageService.Lang));
    }
}
