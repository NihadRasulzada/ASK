using App.BL.DTOs;
using App.BL.Resources;
using App.Core.Interfaces;
using FluentValidation;

namespace App.BL.Validators.Training;

public class CreateTrainingDtoValidator : AbstractValidator<CreateTrainingDto>
{
    private static readonly string[] AllowedContentTypes =
        ["image/jpeg", "image/png", "image/gif", "image/webp"];

    private const long MaxFileSizeBytes = 5 * 1024 * 1024;

    public CreateTrainingDtoValidator(ILanguageService languageService)
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

        RuleFor(x => x.TextAz)
            .NotEmpty().WithMessage(ValidationMessages.TextRequired(languageService.Lang));

        RuleFor(x => x.TextEn)
            .NotEmpty().WithMessage(ValidationMessages.TextRequired(languageService.Lang));

        RuleFor(x => x.TextRu)
            .NotEmpty().WithMessage(ValidationMessages.TextRequired(languageService.Lang));

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
    }
}