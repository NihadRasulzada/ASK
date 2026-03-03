using App.BL.DTOs;
using FluentValidation;

namespace App.BL.Validators;

public class UpdateVideoDtoValidator : AbstractValidator<UpdateVideoDto>
{
    public UpdateVideoDtoValidator()
    {
        RuleFor(x => x.Link)
            .NotEmpty().WithMessage("Link mütləq daxil edilməlidir.")
            .MaximumLength(2048).WithMessage("Link 2048 simvoldan çox ola bilməz.")
            .Must(BeAValidUrl).WithMessage("Link düzgün bir URL olmalıdır (http və ya https).");
    }

    private static bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var result)
               && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
    }
}
