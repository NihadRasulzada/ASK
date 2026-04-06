using App.BL.DTOs;
using App.BL.Resources;
using App.Core.Interfaces;
using FluentValidation;

namespace App.BL.Validators.BusinessForum;

public class CreateBusinessForumDtoValidator : AbstractValidator<CreateBusinessForumDto>
{
    private static readonly string[] AllowedImageTypes =
        ["image/jpeg", "image/png", "image/gif", "image/webp"];

    private const long MaxImageSizeBytes = 5 * 1024 * 1024; // 5 MB

    public CreateBusinessForumDtoValidator(ILanguageService languageService)
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

        RuleFor(x => x.TitleImage)
            .NotNull().WithMessage(ValidationMessages.TitleImageRequired(languageService.Lang));

        When(x => x.TitleImage is not null, () =>
        {
            RuleFor(x => x.TitleImage!)
                .Must(f => f.Length <= MaxImageSizeBytes)
                    .WithMessage(ValidationMessages.TitleImageTooLarge(languageService.Lang))
                .Must(f => AllowedImageTypes.Contains(f.ContentType.ToLower()))
                    .WithMessage(ValidationMessages.ImageInvalidFormat(languageService.Lang));
        });

        RuleFor(x => x.DetailImage)
            .NotNull().WithMessage(ValidationMessages.DetailImageRequired(languageService.Lang));

        When(x => x.DetailImage is not null, () =>
        {
            RuleFor(x => x.DetailImage!)
                .Must(f => f.Length <= MaxImageSizeBytes)
                    .WithMessage(ValidationMessages.ImageTooLarge(languageService.Lang))
                .Must(f => AllowedImageTypes.Contains(f.ContentType.ToLower()))
                    .WithMessage(ValidationMessages.ImageInvalidFormat(languageService.Lang));
        });
    }
}
