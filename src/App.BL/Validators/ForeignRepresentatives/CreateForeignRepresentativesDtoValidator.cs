using App.BL.DTOs;
using App.BL.Resources;
using App.Core.Interfaces;
using FluentValidation;

namespace App.BL.Validators.ForeignRepresentatives;

public class CreateForeignRepresentativesDtoValidator : AbstractValidator<CreateForeignRepresentativesDto>
{
    public CreateForeignRepresentativesDtoValidator(ILanguageService languageService)
    {
        RuleFor(x => x.CountryAz).NotEmpty().WithMessage(ValidationMessages.CountryRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.CountryTooLong(languageService.Lang));
        RuleFor(x => x.CountryEn).NotEmpty().WithMessage(ValidationMessages.CountryRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.CountryTooLong(languageService.Lang));
        RuleFor(x => x.CountryRu).NotEmpty().WithMessage(ValidationMessages.CountryRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.CountryTooLong(languageService.Lang));

        RuleFor(x => x.FullNameAz).NotEmpty().WithMessage(ValidationMessages.FullNameRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.FullNameTooLong(languageService.Lang));
        RuleFor(x => x.FullNameEn).NotEmpty().WithMessage(ValidationMessages.FullNameRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.FullNameTooLong(languageService.Lang));
        RuleFor(x => x.FullNameRu).NotEmpty().WithMessage(ValidationMessages.FullNameRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.FullNameTooLong(languageService.Lang));

        RuleFor(x => x.CompanyAz).NotEmpty().WithMessage(ValidationMessages.CompanyRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.CompanyTooLong(languageService.Lang));
        RuleFor(x => x.CompanyEn).NotEmpty().WithMessage(ValidationMessages.CompanyRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.CompanyTooLong(languageService.Lang));
        RuleFor(x => x.CompanyRu).NotEmpty().WithMessage(ValidationMessages.CompanyRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.CompanyTooLong(languageService.Lang));

        RuleFor(x => x.DutyAz).NotEmpty().WithMessage(ValidationMessages.DutyRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.DutyTooLong(languageService.Lang));
        RuleFor(x => x.DutyEn).NotEmpty().WithMessage(ValidationMessages.DutyRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.DutyTooLong(languageService.Lang));
        RuleFor(x => x.DutyRu).NotEmpty().WithMessage(ValidationMessages.DutyRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.DutyTooLong(languageService.Lang));
    }
}
