using App.BL.DTOs;
using App.BL.Resources;
using App.BL.Validators.Common;
using App.Core.Interfaces;
using FluentValidation;

namespace App.BL.Validators.Video;

public class UpdateVideoDtoValidator : AbstractValidator<UpdateVideoDto>
{
    public UpdateVideoDtoValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Link)
            .NotEmpty().WithMessage(ValidationMessages.LinkRequired(languageService.Lang))
            .MaximumLength(2048).WithMessage(ValidationMessages.LinkTooLong(languageService.Lang))
            // FIX: static helper istifadə olunur — kod təkrarlanmır
            .Must(UrlValidatorHelper.BeAValidUrl)
                .WithMessage(ValidationMessages.LinkInvalidUrl(languageService.Lang));
    }
}