using App.BL.DTOs;
using App.BL.Resources;
using App.Core.Interfaces;
using FluentValidation;

namespace App.BL.Validators.Gallery;

public class UpdateGalleryDtoValidator : AbstractValidator<UpdateGalleryDto>
{
    private static readonly string[] AllowedContentTypes =
        ["image/jpeg", "image/png", "image/gif", "image/webp"];

    private const long MaxFileSizeBytes = 5 * 1024 * 1024;

    public UpdateGalleryDtoValidator(ILanguageService languageService)
    {
        // Image optional-dır — null olarsa mövcud şəkil saxlanılır.
        // Lakin update endpoint-ə Image göndərilmirsə Update-in mənası yoxdur,
        // ona görə NotNull() əlavə etmək də mümkündür — biznes qərarıdır.
        When(x => x.Image is not null, () =>
        {
            RuleFor(x => x.Image!)
                .Must(f => f.Length <= MaxFileSizeBytes)
                    .WithMessage(ValidationMessages.ImageTooLarge(languageService.Lang))
                .Must(f => AllowedContentTypes.Contains(f.ContentType.ToLower()))
                    .WithMessage(ValidationMessages.ImageInvalidFormat(languageService.Lang));
        });
    }
}