using App.BL.DTOs;
using App.BL.Resources;
using App.Core.Interfaces;
using FluentValidation;

namespace App.BL.Validators.Publication;

public class UpdatePublicationDtoValidator : AbstractValidator<UpdatePublicationDto>
{
    private static readonly string[] AllowedImageTypes =
        ["image/jpeg", "image/png", "image/gif", "image/webp"];

    private const long MaxImageSizeBytes = 5 * 1024 * 1024;   // 5 MB
    private const long MaxPdfSizeBytes = 10 * 1024 * 1024;    // 10 MB

    public UpdatePublicationDtoValidator(ILanguageService languageService)
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

        When(x => x.TitleImage is not null, () =>
        {
            RuleFor(x => x.TitleImage!)
                .Must(f => f.Length <= MaxImageSizeBytes)
                    .WithMessage(ValidationMessages.TitleImageTooLarge(languageService.Lang))
                .Must(f => AllowedImageTypes.Contains(f.ContentType.ToLower()))
                    .WithMessage(ValidationMessages.ImageInvalidFormat(languageService.Lang));
        });

        When(x => x.PdfFile is not null, () =>
        {
            RuleFor(x => x.PdfFile!)
                .Must(f => f.Length <= MaxPdfSizeBytes)
                    .WithMessage(ValidationMessages.PdfFileTooLarge(languageService.Lang))
                .Must(f => string.Equals(f.ContentType, "application/pdf", StringComparison.OrdinalIgnoreCase))
                    .WithMessage(ValidationMessages.PdfFileInvalidFormat(languageService.Lang));
        });
    }
}
