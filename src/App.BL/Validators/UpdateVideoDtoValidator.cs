using App.BL.DTOs;
using App.BL.Resources;
using App.Core.Interfaces;
using FluentValidation;

namespace App.BL.Validators;

public class UpdateVideoDtoValidator : AbstractValidator<UpdateVideoDto>
{
    public UpdateVideoDtoValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Link)
            .NotEmpty().WithMessage(ValidationMessages.LinkRequired(languageService.Lang))
            .MaximumLength(2048).WithMessage(ValidationMessages.LinkTooLong(languageService.Lang))
            .Must(BeAValidUrl).WithMessage(ValidationMessages.LinkInvalidUrl(languageService.Lang));
    }

    private static bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var result)
               && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
    }
}
