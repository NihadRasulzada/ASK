using App.BL.DTOs;
using FluentValidation;

namespace App.BL.Validators;

public class CreateDirectorDtoValidator : AbstractValidator<CreateDirectorDto>
{
    private static readonly string[] AllowedContentTypes =
        ["image/jpeg", "image/png", "image/gif", "image/webp"];

    private const long MaxFileSizeBytes = 5 * 1024 * 1024; // 5 MB

    public CreateDirectorDtoValidator()
    {
        RuleFor(x => x.Image)
            .NotNull().WithMessage("Şəkil mütləq daxil edilməlidir.")
            .Must(f => f.Length <= MaxFileSizeBytes)
                .WithMessage("Şəklin ölçüsü 5 MB-dan çox ola bilməz.")
            .Must(f => AllowedContentTypes.Contains(f.ContentType.ToLower()))
                .WithMessage("Yalnız JPEG, PNG, GIF və ya WebP formatında şəkil yüklənə bilər.");

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Tam ad mütləq daxil edilməlidir.")
            .MaximumLength(200).WithMessage("Tam ad 200 simvoldan çox ola bilməz.");

        RuleFor(x => x.Duty)
            .NotEmpty().WithMessage("Vəzifə mütləq daxil edilməlidir.")
            .MaximumLength(200).WithMessage("Vəzifə 200 simvoldan çox ola bilməz.");
    }
}
