using Microsoft.AspNetCore.Http;

namespace App.BL.DTOs;

// FIX: IFormFile saxlayan DTO record deyil, class olmalıdır
public class CreatePresidentDto
{
    public IFormFile Image { get; set; } = null!;
    public string Text { get; set; } = string.Empty;
}

public class UpdatePresidentDto
{
    public IFormFile? Image { get; set; }
    public string Text { get; set; } = string.Empty;
}

public record PresidentResponseDto(Guid Id, string ImageUrl, string Text);