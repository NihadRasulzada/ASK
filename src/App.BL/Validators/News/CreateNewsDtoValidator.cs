using App.BL.DTOs;
using App.BL.Resources;
using App.Core.Interfaces;
using FluentValidation;

namespace App.BL.Validators.News;

public class CreateNewsDtoValidator : AbstractValidator<CreateNewsDto>
{
    private static readonly string[] AllowedContentTypes =
        ["image/jpeg", "image/png", "image/gif", "image/webp"];

    private const long MaxFileSizeBytes = 5 * 1024 * 1024;

    public CreateNewsDtoValidator(ILanguageService languageService)
    {
        // FIX: NotNull() ayrı; size/type yoxlaması When() bloku içindədir
        RuleFor(x => x.TitleImage)
            .NotNull().WithMessage(ValidationMessages.TitleImageRequired(languageService.Lang));

        When(x => x.TitleImage is not null, () =>
        {
            RuleFor(x => x.TitleImage!)
                .Must(f => f.Length <= MaxFileSizeBytes)
                    .WithMessage(ValidationMessages.TitleImageTooLarge(languageService.Lang))
                .Must(f => AllowedContentTypes.Contains(f.ContentType.ToLower()))
                    .WithMessage(ValidationMessages.ImageInvalidFormat(languageService.Lang));
        });

        RuleFor(x => x.TitleAz)
            .NotEmpty().WithMessage(ValidationMessages.TitleRequired(languageService.Lang))
            .MaximumLength(500).WithMessage(ValidationMessages.TitleTooLong(languageService.Lang));

        RuleFor(x => x.TitleEn)
            .NotEmpty().WithMessage(ValidationMessages.TitleRequired(languageService.Lang))
            .MaximumLength(500).WithMessage(ValidationMessages.TitleTooLong(languageService.Lang));

        RuleFor(x => x.TitleRu)
            .NotEmpty().WithMessage(ValidationMessages.TitleRequired(languageService.Lang))
            .MaximumLength(500).WithMessage(ValidationMessages.TitleTooLong(languageService.Lang));

        // FIX: Yalnız Az deyil, En və Ru sahələri də validasiya edilir
        RuleFor(x => x.NewsTextAz)
            .NotEmpty().WithMessage(ValidationMessages.NewsTextRequired(languageService.Lang))
            .MaximumLength(10000).WithMessage(ValidationMessages.NewsTextTooLong(languageService.Lang));

        RuleFor(x => x.NewsTextEn)
            .NotEmpty().WithMessage(ValidationMessages.NewsTextRequired(languageService.Lang))
            .MaximumLength(10000).WithMessage(ValidationMessages.NewsTextTooLong(languageService.Lang));

        RuleFor(x => x.NewsTextRu)
            .NotEmpty().WithMessage(ValidationMessages.NewsTextRequired(languageService.Lang))
            .MaximumLength(10000).WithMessage(ValidationMessages.NewsTextTooLong(languageService.Lang));
    }
}