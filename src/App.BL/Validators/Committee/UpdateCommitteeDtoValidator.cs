using App.BL.DTOs;
using App.BL.Resources;
using App.Core.Interfaces;
using FluentValidation;

namespace App.BL.Validators.Committee;

public class UpdateCommitteeDtoValidator : AbstractValidator<UpdateCommitteeDto>
{
    public UpdateCommitteeDtoValidator(ILanguageService languageService)
    {
        RuleFor(x => x.NameAz).NotEmpty().WithMessage(ValidationMessages.NameRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.NameTooLong(languageService.Lang));
        RuleFor(x => x.NameEn).NotEmpty().WithMessage(ValidationMessages.NameRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.NameTooLong(languageService.Lang));
        RuleFor(x => x.NameRu).NotEmpty().WithMessage(ValidationMessages.NameRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.NameTooLong(languageService.Lang));

        RuleFor(x => x.ChairmanAz).NotEmpty().WithMessage(ValidationMessages.ChairmanRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.ChairmanTooLong(languageService.Lang));
        RuleFor(x => x.ChairmanEn).NotEmpty().WithMessage(ValidationMessages.ChairmanRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.ChairmanTooLong(languageService.Lang));
        RuleFor(x => x.ChairmanRu).NotEmpty().WithMessage(ValidationMessages.ChairmanRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.ChairmanTooLong(languageService.Lang));

        RuleFor(x => x.VicePresidentAz).NotEmpty().WithMessage(ValidationMessages.VicePresidentRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.VicePresidentTooLong(languageService.Lang));
        RuleFor(x => x.VicePresidentEn).NotEmpty().WithMessage(ValidationMessages.VicePresidentRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.VicePresidentTooLong(languageService.Lang));
        RuleFor(x => x.VicePresidentRu).NotEmpty().WithMessage(ValidationMessages.VicePresidentRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.VicePresidentTooLong(languageService.Lang));
    }
}
