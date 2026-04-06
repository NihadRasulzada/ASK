using App.BL.DTOs;
using App.BL.Resources;
using App.Core.Interfaces;
using FluentValidation;

namespace App.BL.Validators.FAQ;

public class UpdateFAQDtoValidator : AbstractValidator<UpdateFAQDto>
{
    public UpdateFAQDtoValidator(ILanguageService languageService)
    {
        RuleFor(x => x.QuestionAz)
            .NotEmpty().WithMessage(ValidationMessages.QuestionRequired(languageService.Lang))
            .MaximumLength(1000).WithMessage(ValidationMessages.QuestionTooLong(languageService.Lang));

        RuleFor(x => x.QuestionEn)
            .NotEmpty().WithMessage(ValidationMessages.QuestionRequired(languageService.Lang))
            .MaximumLength(1000).WithMessage(ValidationMessages.QuestionTooLong(languageService.Lang));

        RuleFor(x => x.QuestionRu)
            .NotEmpty().WithMessage(ValidationMessages.QuestionRequired(languageService.Lang))
            .MaximumLength(1000).WithMessage(ValidationMessages.QuestionTooLong(languageService.Lang));

        RuleFor(x => x.AnswerAz)
            .NotEmpty().WithMessage(ValidationMessages.AnswerRequired(languageService.Lang))
            .MaximumLength(5000).WithMessage(ValidationMessages.AnswerTooLong(languageService.Lang));

        RuleFor(x => x.AnswerEn)
            .NotEmpty().WithMessage(ValidationMessages.AnswerRequired(languageService.Lang))
            .MaximumLength(5000).WithMessage(ValidationMessages.AnswerTooLong(languageService.Lang));

        RuleFor(x => x.AnswerRu)
            .NotEmpty().WithMessage(ValidationMessages.AnswerRequired(languageService.Lang))
            .MaximumLength(5000).WithMessage(ValidationMessages.AnswerTooLong(languageService.Lang));
    }
}
