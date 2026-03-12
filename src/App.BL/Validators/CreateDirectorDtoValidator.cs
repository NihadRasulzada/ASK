using App.BL.DTOs;
using App.BL.Resources;
using App.Core.Interfaces;
using FluentValidation;

namespace App.BL.Validators;

public class CreateDirectorDtoValidator : AbstractValidator<CreateDirectorDto>
{
    private static readonly string[] AllowedContentTypes =
        ["image/jpeg", "image/png", "image/gif", "image/webp"];

    private const long MaxFileSizeBytes = 5 * 1024 * 1024;

    public CreateDirectorDtoValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Image)
            .NotNull().WithMessage(ValidationMessages.ImageRequired(languageService.Lang))
            .Must(f => f.Length <= MaxFileSizeBytes)
                .WithMessage(ValidationMessages.ImageTooLarge(languageService.Lang))
            .Must(f => AllowedContentTypes.Contains(f.ContentType.ToLower()))
                .WithMessage(ValidationMessages.ImageInvalidFormat(languageService.Lang));

        RuleFor(x => x.FullNameAz)
            .NotEmpty().WithMessage(ValidationMessages.FullNameRequired(languageService.Lang))
            .MaximumLength(200).WithMessage(ValidationMessages.FullNameTooLong(languageService.Lang));

        RuleFor(x => x.DutyAz)
            .NotEmpty().WithMessage(ValidationMessages.DutyRequired(languageService.Lang))
            .MaximumLength(200).WithMessage(ValidationMessages.DutyTooLong(languageService.Lang));
    }
}
