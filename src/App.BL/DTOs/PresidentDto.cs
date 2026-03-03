using Microsoft.AspNetCore.Http;

namespace App.BL.DTOs;

public record CreatePresidentDto
{
    public IFormFile Image { get; set; } = null!;
    public string Text { get; set; } = string.Empty;
}

public record UpdatePresidentDto
{
    public IFormFile? Image { get; set; }
    public string Text { get; set; } = string.Empty;
}

public record PresidentResponseDto(Guid Id, string ImageUrl, string Text);
