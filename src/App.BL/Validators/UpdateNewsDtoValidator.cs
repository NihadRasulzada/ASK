using App.BL.DTOs;
using App.BL.Resources;
using App.Core.Interfaces;
using FluentValidation;

namespace App.BL.Validators;

public class UpdateNewsDtoValidator : AbstractValidator<UpdateNewsDto>
{
    private static readonly string[] AllowedContentTypes =
        ["image/jpeg", "image/png", "image/gif", "image/webp"];

    private const long MaxFileSizeBytes = 5 * 1024 * 1024;

    public UpdateNewsDtoValidator(ILanguageService languageService)
    {
        When(x => x.TitleImage is not null, () =>
        {
            RuleFor(x => x.TitleImage!)
                .Must(f => f.Length <= MaxFileSizeBytes)
                    .WithMessage(ValidationMessages.TitleImageTooLarge(languageService.Lang))
                .Must(f => AllowedContentTypes.Contains(f.ContentType.ToLower()))
                    .WithMessage(ValidationMessages.ImageInvalidFormat(languageService.Lang));
        });

        RuleFor(x => x.NewsTextAz)
            .NotEmpty().WithMessage(ValidationMessages.NewsTextRequired(languageService.Lang))
            .MaximumLength(10000).WithMessage(ValidationMessages.NewsTextTooLong(languageService.Lang));

        When(x => x.AdditionalImages is not null && x.AdditionalImages.Count > 0, () =>
        {
            RuleForEach(x => x.AdditionalImages)
                .Must(f => f.Length <= MaxFileSizeBytes)
                    .WithMessage(ValidationMessages.AdditionalImageTooLarge(languageService.Lang))
                .Must(f => AllowedContentTypes.Contains(f.ContentType.ToLower()))
                    .WithMessage(ValidationMessages.ImageInvalidFormat(languageService.Lang));
        });
    }
}
