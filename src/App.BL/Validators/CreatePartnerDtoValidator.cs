using App.BL.DTOs;
using FluentValidation;

namespace App.BL.Validators;

public class CreatePartnerDtoValidator : AbstractValidator<CreatePartnerDto>
{
    private static readonly string[] AllowedContentTypes =
        ["image/jpeg", "image/png", "image/gif", "image/webp"];

    private const long MaxFileSizeBytes = 5 * 1024 * 1024; // 5 MB

    public CreatePartnerDtoValidator()
    {
        RuleFor(x => x.Image)
            .NotNull().WithMessage("Şəkil mütləq daxil edilməlidir.")
            .Must(f => f.Length <= MaxFileSizeBytes)
                .WithMessage("Şəkil ölçüsü 5 MB-dan çox ola bilməz.")
            .Must(f => AllowedContentTypes.Contains(f.ContentType.ToLower()))
                .WithMessage("Yalnız JPEG, PNG, GIF və ya WebP formatında şəkil yüklənə bilər.");

        RuleFor(x => x.Site)
            .NotEmpty().WithMessage("Sayt URL-i mütləq daxil edilməlidir.")
            .MaximumLength(2048).WithMessage("Sayt URL-i 2048 simvoldan çox ola bilməz.")
            .Must(BeAValidUrl).WithMessage("Sayt URL-i düzgün bir URL olmalıdır (http və ya https).");
    }

    private static bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var result)
               && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
    }
}
