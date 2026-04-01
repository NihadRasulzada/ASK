using App.BL.DTOs;
using App.BL.Resources;
using App.Core.Interfaces;
using FluentValidation;

namespace App.BL.Validators.Management;

public class CreateManagementDtoValidator : AbstractValidator<CreateManagementDto>
{
    public CreateManagementDtoValidator(ILanguageService languageService)
    {
        RuleFor(x => x.FullNameAz)
            .NotEmpty().WithMessage(ValidationMessages.FullNameRequired(languageService.Lang))
            .MaximumLength(200).WithMessage(ValidationMessages.FullNameTooLong(languageService.Lang));
        RuleFor(x => x.FullNameEn)
            .NotEmpty().WithMessage(ValidationMessages.FullNameRequired(languageService.Lang))
            .MaximumLength(200).WithMessage(ValidationMessages.FullNameTooLong(languageService.Lang));
        RuleFor(x => x.FullNameRu)
            .NotEmpty().WithMessage(ValidationMessages.FullNameRequired(languageService.Lang))
            .MaximumLength(200).WithMessage(ValidationMessages.FullNameTooLong(languageService.Lang));

        RuleFor(x => x.CompanyAz)
            .NotEmpty().WithMessage(ValidationMessages.CompanyRequired(languageService.Lang))
            .MaximumLength(200).WithMessage(ValidationMessages.CompanyTooLong(languageService.Lang));
        RuleFor(x => x.CompanyEn)
            .NotEmpty().WithMessage(ValidationMessages.CompanyRequired(languageService.Lang))
            .MaximumLength(200).WithMessage(ValidationMessages.CompanyTooLong(languageService.Lang));
        RuleFor(x => x.CompanyRu)
            .NotEmpty().WithMessage(ValidationMessages.CompanyRequired(languageService.Lang))
            .MaximumLength(200).WithMessage(ValidationMessages.CompanyTooLong(languageService.Lang));
    }
}
