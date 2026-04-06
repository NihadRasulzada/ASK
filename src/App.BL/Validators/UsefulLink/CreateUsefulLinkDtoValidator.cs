using App.BL.DTOs;
using App.BL.Resources;
using App.BL.Validators.Common;
using App.Core.Interfaces;
using FluentValidation;

namespace App.BL.Validators.UsefulLink;

public class CreateUsefulLinkDtoValidator : AbstractValidator<CreateUsefulLinkDto>
{
    public CreateUsefulLinkDtoValidator(ILanguageService languageService)
    {
        RuleFor(x => x.TitleAz)
            .NotEmpty().WithMessage(ValidationMessages.TitleRequired(languageService.Lang))
            .MaximumLength(300).WithMessage(ValidationMessages.TitleTooLong(languageService.Lang));

        RuleFor(x => x.TitleEn)
            .NotEmpty().WithMessage(ValidationMessages.TitleRequired(languageService.Lang))
            .MaximumLength(300).WithMessage(ValidationMessages.TitleTooLong(languageService.Lang));

        RuleFor(x => x.TitleRu)
            .NotEmpty().WithMessage(ValidationMessages.TitleRequired(languageService.Lang))
            .MaximumLength(300).WithMessage(ValidationMessages.TitleTooLong(languageService.Lang));

        RuleFor(x => x.Link)
            .NotEmpty().WithMessage(ValidationMessages.LinkRequired(languageService.Lang))
            .MaximumLength(2048).WithMessage(ValidationMessages.LinkTooLong(languageService.Lang))
            .Must(UrlValidatorHelper.BeAValidUrl)
                .WithMessage(ValidationMessages.LinkInvalidUrl(languageService.Lang));
    }
}
