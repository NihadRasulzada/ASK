using App.BL.DTOs;
using App.BL.Resources;
using App.Core.Interfaces;
using FluentValidation;

namespace App.BL.Validators.Director;

public class UpdateDirectorDtoValidator : AbstractValidator<UpdateDirectorDto>
{
    private static readonly string[] AllowedContentTypes =
        ["image/jpeg", "image/png", "image/gif", "image/webp"];

    private const long MaxFileSizeBytes = 5 * 1024 * 1024;

    public UpdateDirectorDtoValidator(ILanguageService languageService)
    {
        When(x => x.Image is not null, () =>
        {
            RuleFor(x => x.Image!)
                .Must(f => f.Length <= MaxFileSizeBytes)
                    .WithMessage(ValidationMessages.ImageTooLarge(languageService.Lang))
                .Must(f => AllowedContentTypes.Contains(f.ContentType.ToLower()))
                    .WithMessage(ValidationMessages.ImageInvalidFormat(languageService.Lang));
        });

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

        When(x => x.DepartmentAz is not null, () =>
        {
            RuleFor(x => x.DepartmentAz!)
                .MaximumLength(200).WithMessage(ValidationMessages.NameTooLong(languageService.Lang));
        });

        When(x => x.DepartmentEn is not null, () =>
        {
            RuleFor(x => x.DepartmentEn!)
                .MaximumLength(200).WithMessage(ValidationMessages.NameTooLong(languageService.Lang));
        });

        When(x => x.DepartmentRu is not null, () =>
        {
            RuleFor(x => x.DepartmentRu!)
                .MaximumLength(200).WithMessage(ValidationMessages.NameTooLong(languageService.Lang));
        });

        When(x => x.PhoneNumber is not null, () =>
        {
            RuleFor(x => x.PhoneNumber!)
                .MaximumLength(50).WithMessage(ValidationMessages.PhoneNumberTooLong(languageService.Lang))
                .Matches(@"^\+?[0-9\s\-\(\)]+$").WithMessage(ValidationMessages.PhoneNumberInvalidFormat(languageService.Lang));
        });

        When(x => x.Email is not null, () =>
        {
            RuleFor(x => x.Email!)
                .EmailAddress().WithMessage(ValidationMessages.EmailInvalidFormat(languageService.Lang))
                .MaximumLength(256).WithMessage(ValidationMessages.EmailTooLong(languageService.Lang));
        });
    }
}