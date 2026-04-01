using App.BL.DTOs;
using App.BL.Resources;
using App.Core.Interfaces;
using FluentValidation;

namespace App.BL.Validators.Presidium;

public class UpdatePresidiumDtoValidator : AbstractValidator<UpdatePresidiumDto>
{
    private static readonly string[] AllowedContentTypes =
        ["image/jpeg", "image/png", "image/gif", "image/webp"];
    private const long MaxFileSizeBytes = 5 * 1024 * 1024;

    public UpdatePresidiumDtoValidator(ILanguageService languageService)
    {
        When(x => x.Image is not null, () =>
        {
            RuleFor(x => x.Image!)
                .Must(f => f.Length <= MaxFileSizeBytes)
                    .WithMessage(ValidationMessages.ImageTooLarge(languageService.Lang))
                .Must(f => AllowedContentTypes.Contains(f.ContentType.ToLower()))
                    .WithMessage(ValidationMessages.ImageInvalidFormat(languageService.Lang));
        });

        RuleFor(x => x.FullNameAz).NotEmpty().WithMessage(ValidationMessages.FullNameRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.FullNameTooLong(languageService.Lang));
        RuleFor(x => x.FullNameEn).NotEmpty().WithMessage(ValidationMessages.FullNameRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.FullNameTooLong(languageService.Lang));
        RuleFor(x => x.FullNameRu).NotEmpty().WithMessage(ValidationMessages.FullNameRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.FullNameTooLong(languageService.Lang));

        RuleFor(x => x.PositionAz).NotEmpty().WithMessage(ValidationMessages.PositionRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.PositionTooLong(languageService.Lang));
        RuleFor(x => x.PositionEn).NotEmpty().WithMessage(ValidationMessages.PositionRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.PositionTooLong(languageService.Lang));
        RuleFor(x => x.PositionRu).NotEmpty().WithMessage(ValidationMessages.PositionRequired(languageService.Lang)).MaximumLength(200).WithMessage(ValidationMessages.PositionTooLong(languageService.Lang));
    }
}
