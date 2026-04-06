using App.BL.DTOs;
using App.BL.Resources;
using App.Core.Interfaces;
using FluentValidation;

namespace App.BL.Validators.FAQ;

public class SubmitFAQInquiryDtoValidator : AbstractValidator<SubmitFAQInquiryDto>
{
    public SubmitFAQInquiryDtoValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Question)
            .NotEmpty().WithMessage(ValidationMessages.QuestionRequired(languageService.Lang))
            .MaximumLength(1000).WithMessage(ValidationMessages.QuestionTooLong(languageService.Lang));
    }
}
