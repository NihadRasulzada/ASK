using App.BL.DTOs;
using App.BL.Resources;
using App.Core.Interfaces;
using FluentValidation;

namespace App.BL.Validators.OurValues;

public class CreateOurValuesDtoValidator : AbstractValidator<CreateOurValuesDto>
{
    private static readonly string[] AllowedContentTypes =
        ["image/jpeg", "image/png", "image/gif", "image/webp"];
    private const long MaxFileSizeBytes = 5 * 1024 * 1024;

    public CreateOurValuesDtoValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Image)
            .NotNull().WithMessage(ValidationMessages.ImageRequired(languageService.Lang));

        When(x => x.Image is not null, () =>
        {
            RuleFor(x => x.Image!)
                .Must(f => f.Length <= MaxFileSizeBytes)
                    .WithMessage(ValidationMessages.ImageTooLarge(languageService.Lang))
                .Must(f => AllowedContentTypes.Contains(f.ContentType.ToLower()))
                    .WithMessage(ValidationMessages.ImageInvalidFormat(languageService.Lang));
        });

        RuleFor(x => x.TitleAz).NotEmpty().WithMessage(ValidationMessages.TitleRequired(languageService.Lang)).MaximumLength(500).WithMessage(ValidationMessages.TitleTooLong(languageService.Lang));
        RuleFor(x => x.TitleEn).NotEmpty().WithMessage(ValidationMessages.TitleRequired(languageService.Lang)).MaximumLength(500).WithMessage(ValidationMessages.TitleTooLong(languageService.Lang));
        RuleFor(x => x.TitleRu).NotEmpty().WithMessage(ValidationMessages.TitleRequired(languageService.Lang)).MaximumLength(500).WithMessage(ValidationMessages.TitleTooLong(languageService.Lang));
    }
}
