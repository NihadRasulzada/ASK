using App.BL.DTOs;
using App.BL.Resources;
using App.BL.Validators.Common;
using App.Core.Interfaces;
using FluentValidation;

namespace App.BL.Validators.Director;

public class CreateDirectorDtoValidator : AbstractValidator<CreateDirectorDto>
{
    private static readonly string[] AllowedContentTypes =
        ["image/jpeg", "image/png", "image/gif", "image/webp"];

    private const long MaxFileSizeBytes = 5 * 1024 * 1024;

    public CreateDirectorDtoValidator(ILanguageService languageService)
    {
        // FIX: NotNull() ayrı; size/type yoxlaması When() bloku içindədir —
        //      NullReferenceException riski aradan qalxır.
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

        // FIX: Yalnız Az deyil, En və Ru sahələri də validasiya edilir
        RuleFor(x => x.FullNameAz)
            .NotEmpty().WithMessage(ValidationMessages.FullNameRequired(languageService.Lang))
            .MaximumLength(200).WithMessage(ValidationMessages.FullNameTooLong(languageService.Lang));

        RuleFor(x => x.FullNameEn)
            .NotEmpty().WithMessage(ValidationMessages.FullNameRequired(languageService.Lang))
            .MaximumLength(200).WithMessage(ValidationMessages.FullNameTooLong(languageService.Lang));

        RuleFor(x => x.FullNameRu)
            .NotEmpty().WithMessage(ValidationMessages.FullNameRequired(languageService.Lang))
            .MaximumLength(200).WithMessage(ValidationMessages.FullNameTooLong(languageService.Lang));

        RuleFor(x => x.DutyAz)
            .NotEmpty().WithMessage(ValidationMessages.DutyRequired(languageService.Lang))
            .MaximumLength(200).WithMessage(ValidationMessages.DutyTooLong(languageService.Lang));

        RuleFor(x => x.DutyEn)
            .NotEmpty().WithMessage(ValidationMessages.DutyRequired(languageService.Lang))
            .MaximumLength(200).WithMessage(ValidationMessages.DutyTooLong(languageService.Lang));

        RuleFor(x => x.DutyRu)
            .NotEmpty().WithMessage(ValidationMessages.DutyRequired(languageService.Lang))
            .MaximumLength(200).WithMessage(ValidationMessages.DutyTooLong(languageService.Lang));
    }
}