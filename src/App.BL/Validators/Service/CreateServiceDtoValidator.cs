using App.BL.DTOs;
using App.BL.Resources;
using App.Core.Interfaces;
using FluentValidation;

namespace App.BL.Validators.Service;

public class CreateServiceDtoValidator : AbstractValidator<CreateServiceDto>
{
    private static readonly string[] AllowedContentTypes =
        ["image/jpeg", "image/png", "image/gif", "image/webp"];

    private const long MaxFileSizeBytes = 5 * 1024 * 1024;

    public CreateServiceDtoValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Image)
            .NotNull().WithMessage(ValidationMessages.ImageRequired(languageService.Lang))
            .Must(f => f.Length <= MaxFileSizeBytes)
                .WithMessage(ValidationMessages.ImageTooLarge(languageService.Lang))
            .Must(f => AllowedContentTypes.Contains(f.ContentType.ToLower()))
                .WithMessage(ValidationMessages.ImageInvalidFormat(languageService.Lang));

        RuleFor(x => x.NameAz)
            .NotEmpty().WithMessage(ValidationMessages.ServiceNameAzRequired(languageService.Lang))
            .MaximumLength(200).WithMessage(ValidationMessages.ServiceNameAzTooLong(languageService.Lang));

        RuleFor(x => x.NameEn)
            .NotEmpty().WithMessage(ValidationMessages.ServiceNameEnRequired(languageService.Lang))
            .MaximumLength(200).WithMessage(ValidationMessages.ServiceNameEnTooLong(languageService.Lang));

        RuleFor(x => x.NameRu)
            .NotEmpty().WithMessage(ValidationMessages.ServiceNameRuRequired(languageService.Lang))
            .MaximumLength(200).WithMessage(ValidationMessages.ServiceNameRuTooLong(languageService.Lang));
    }
}