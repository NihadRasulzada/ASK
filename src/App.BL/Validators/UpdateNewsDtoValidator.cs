using App.BL.DTOs;
using FluentValidation;

namespace App.BL.Validators;

public class UpdateNewsDtoValidator : AbstractValidator<UpdateNewsDto>
{
    private static readonly string[] AllowedContentTypes =
        ["image/jpeg", "image/png", "image/gif", "image/webp"];

    private const long MaxFileSizeBytes = 5 * 1024 * 1024; // 5 MB

    public UpdateNewsDtoValidator()
    {
        // Başlıq şəkli könüllüdür — göndərildikdə yoxlanır
        When(x => x.TitleImage is not null, () =>
        {
            RuleFor(x => x.TitleImage!)
                .Must(f => f.Length <= MaxFileSizeBytes)
                    .WithMessage("Başlıq şəklinin ölçüsü 5 MB-dan çox ola bilməz.")
                .Must(f => AllowedContentTypes.Contains(f.ContentType.ToLower()))
                    .WithMessage("Yalnız JPEG, PNG, GIF və ya WebP formatında şəkil yüklənə bilər.");
        });

        RuleFor(x => x.NewsText)
            .NotEmpty().WithMessage("Xəbər mətni mütləq daxil edilməlidir.")
            .MaximumLength(10000).WithMessage("Xəbər mətni 10000 simvoldan çox ola bilməz.");

        // Əlavə şəkillər könüllüdür — göndərildikdə hər biri yoxlanır
        When(x => x.AdditionalImages is not null && x.AdditionalImages.Count > 0, () =>
        {
            RuleForEach(x => x.AdditionalImages)
                .Must(f => f.Length <= MaxFileSizeBytes)
                    .WithMessage("Hər əlavə şəklin ölçüsü 5 MB-dan çox ola bilməz.")
                .Must(f => AllowedContentTypes.Contains(f.ContentType.ToLower()))
                    .WithMessage("Yalnız JPEG, PNG, GIF və ya WebP formatında şəkil yüklənə bilər.");
        });
    }
}
