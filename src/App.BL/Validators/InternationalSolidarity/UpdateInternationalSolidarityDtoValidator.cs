using App.BL.DTOs;
using App.BL.Resources;
using App.Core.Interfaces;
using FluentValidation;

namespace App.BL.Validators.InternationalSolidarity;

public class UpdateInternationalSolidarityDtoValidator : AbstractValidator<UpdateInternationalSolidarityDto>
{
    private static readonly string[] AllowedContentTypes =
        ["image/jpeg", "image/png", "image/gif", "image/webp"];
    private const long MaxFileSizeBytes = 5 * 1024 * 1024;

    public UpdateInternationalSolidarityDtoValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Link)
            .NotEmpty().WithMessage(ValidationMessages.LinkRequired(languageService.Lang))
            .MaximumLength(2048).WithMessage(ValidationMessages.LinkTooLong(languageService.Lang))
            .Must(url => Uri.TryCreate(url, UriKind.Absolute, out var uri) &&
                         (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
            .WithMessage(ValidationMessages.LinkInvalidUrl(languageService.Lang));

        When(x => x.Icon is not null, () =>
        {
            RuleFor(x => x.Icon!)
                .Must(f => f.Length <= MaxFileSizeBytes)
                    .WithMessage(ValidationMessages.ImageTooLarge(languageService.Lang))
                .Must(f => AllowedContentTypes.Contains(f.ContentType.ToLower()))
                    .WithMessage(ValidationMessages.ImageInvalidFormat(languageService.Lang));
        });
    }
}
