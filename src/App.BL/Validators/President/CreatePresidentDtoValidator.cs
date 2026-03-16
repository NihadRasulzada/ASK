using App.BL.DTOs;
using App.BL.Resources;
using App.Core.Interfaces;
using FluentValidation;

namespace App.BL.Validators.President;

public class CreatePresidentDtoValidator : AbstractValidator<CreatePresidentDto>
{
    private static readonly string[] AllowedContentTypes =
        ["image/jpeg", "image/png", "image/gif", "image/webp"];

    private const long MaxFileSizeBytes = 5 * 1024 * 1024;

    public CreatePresidentDtoValidator(ILanguageService languageService)
    {
        // FIX: NotNull() ayrı; size/type yoxlaması When() bloku içindədir
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

        RuleFor(x => x.Text)
            .NotEmpty().WithMessage(ValidationMessages.TextRequired(languageService.Lang))
            .MaximumLength(5000).WithMessage(ValidationMessages.TextTooLong(languageService.Lang));
    }
}