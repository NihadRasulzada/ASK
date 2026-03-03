using App.BL.DTOs;
using FluentValidation;

namespace App.BL.Validators;

public class CreatePresidentDtoValidator : AbstractValidator<CreatePresidentDto>
{
    private static readonly string[] AllowedContentTypes =
        ["image/jpeg", "image/png", "image/gif", "image/webp"];

    private const long MaxFileSizeBytes = 5 * 1024 * 1024; // 5 MB

    public CreatePresidentDtoValidator()
    {
        RuleFor(x => x.Image)
            .NotNull().WithMessage("Şəkil mütləq daxil edilməlidir.")
            .Must(f => f.Length <= MaxFileSizeBytes)
                .WithMessage("Şəklin ölçüsü 5 MB-dan çox ola bilməz.")
            .Must(f => AllowedContentTypes.Contains(f.ContentType.ToLower()))
                .WithMessage("Yalnız JPEG, PNG, GIF və ya WebP formatında şəkil yüklənə bilər.");

        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Mətn mütləq daxil edilməlidir.")
            .MaximumLength(5000).WithMessage("Mətn 5000 simvoldan çox ola bilməz.");
    }
}
