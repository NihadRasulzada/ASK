using App.Core.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace App.BL.Validators.Common;

/// <summary>
/// IFormFile validation qaydalarını mərkəzləşdirir.
/// Bütün image validator-lar bu class-dan inherit edir.
/// </summary>
public abstract class BaseImageValidator<T> : AbstractValidator<T>
    where T : class
{
    protected static readonly string[] AllowedContentTypes =
        ["image/jpeg", "image/png", "image/gif", "image/webp"];

    protected const long MaxFileSizeBytes = 5 * 1024 * 1024; // 5 MB

    /// <summary>
    /// FIX: NotNull() sonra Must(f => f.Length) zənciri NullReferenceException atar.
    /// Düzgün yanaşma: NotNull() ayrı, sonra When() bloku içində size/type yoxlaması.
    /// </summary>
    protected void AddRequiredImageRules(
        IRuleBuilderInitial<T, IFormFile> ruleBuilder,
        string requiredMsg,
        string tooLargeMsg,
        string invalidFormatMsg)
    {
        ruleBuilder
            .NotNull().WithMessage(requiredMsg);
    }

    protected void AddOptionalImageRules(
        ILanguageService languageService,
        Func<T, IFormFile?> imageSelector,
        string tooLargeMsg,
        string invalidFormatMsg)
    {
        When(x => imageSelector(x) is not null, () =>
        {
            RuleFor(x => imageSelector(x)!)
                .Must(f => f.Length <= MaxFileSizeBytes)
                    .WithMessage(tooLargeMsg)
                .Must(f => AllowedContentTypes.Contains(f.ContentType.ToLower()))
                    .WithMessage(invalidFormatMsg);
        });
    }
}