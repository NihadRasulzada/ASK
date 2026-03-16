using App.BL.DTOs;
using App.BL.Resources;
using App.Core.Interfaces;
using FluentValidation;

namespace App.BL.Validators.Partner;

public class UpdatePartnerDtoValidator : AbstractValidator<UpdatePartnerDto>
{
    private static readonly string[] AllowedContentTypes =
        ["image/jpeg", "image/png", "image/gif", "image/webp"];

    private const long MaxFileSizeBytes = 5 * 1024 * 1024;

    public UpdatePartnerDtoValidator(ILanguageService languageService)
    {
        When(x => x.Image is not null, () =>
        {
            RuleFor(x => x.Image!)
                .Must(f => f.Length <= MaxFileSizeBytes)
                    .WithMessage(ValidationMessages.ImageTooLarge(languageService.Lang))
                .Must(f => AllowedContentTypes.Contains(f.ContentType.ToLower()))
                    .WithMessage(ValidationMessages.ImageInvalidFormat(languageService.Lang));
        });

        RuleFor(x => x.Site)
            .NotEmpty().WithMessage(ValidationMessages.SiteRequired(languageService.Lang))
            .MaximumLength(2048).WithMessage(ValidationMessages.SiteTooLong(languageService.Lang))
            .Must(BeAValidUrl).WithMessage(ValidationMessages.SiteInvalidUrl(languageService.Lang));
    }

    private static bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var result)
               && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
    }
}
