using Microsoft.AspNetCore.Http;

namespace App.BL.DTOs;

public class CreateOurValuesDto
{
    public string TitleAz { get; set; } = string.Empty;
    public string TitleEn { get; set; } = string.Empty;
    public string TitleRu { get; set; } = string.Empty;
    public IFormFile Image { get; set; } = null!;
}

public class UpdateOurValuesDto
{
    public string TitleAz { get; set; } = string.Empty;
    public string TitleEn { get; set; } = string.Empty;
    public string TitleRu { get; set; } = string.Empty;
    public IFormFile? Image { get; set; }
}

public record OurValuesResponseDto(
    Guid Id,
    string Title,
    string ImageUrl);
