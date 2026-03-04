using Microsoft.AspNetCore.Http;

namespace App.BL.DTOs;

public record CreateDirectorDto
{
    public IFormFile Image { get; set; } = null!;
    public string FullName { get; set; } = string.Empty;
    public string Duty { get; set; } = string.Empty;
}

public record UpdateDirectorDto
{
    public IFormFile? Image { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Duty { get; set; } = string.Empty;
}

public record DirectorResponseDto(Guid Id, string ImageUrl, string FullName, string Duty);
