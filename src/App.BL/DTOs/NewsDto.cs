using Microsoft.AspNetCore.Http;

namespace App.BL.DTOs;

// FIX: IFormFile saxladığı üçün class (record deyil)
public class CreateNewsDto
{
    public IFormFile TitleImage { get; set; } = null!;
    public string TitleAz { get; set; } = string.Empty;
    public string TitleEn { get; set; } = string.Empty;
    public string TitleRu { get; set; } = string.Empty;
    public string NewsTextAz { get; set; } = string.Empty;
    public string NewsTextEn { get; set; } = string.Empty;
    public string NewsTextRu { get; set; } = string.Empty;
}

public class UpdateNewsDto
{
    public IFormFile? TitleImage { get; set; }
    public string TitleAz { get; set; } = string.Empty;
    public string TitleEn { get; set; } = string.Empty;
    public string TitleRu { get; set; } = string.Empty;
    public string NewsTextAz { get; set; } = string.Empty;
    public string NewsTextEn { get; set; } = string.Empty;
    public string NewsTextRu { get; set; } = string.Empty;
}

/// <param name="ImageUrls">NewsImage entity-sindən gələn URL siyahısı.</param>
public record NewsResponseDto(
    Guid Id,
    string TitleImageUrl,
    string Title,
    string NewsText,
    IList<string> ImageUrls,
    bool IsDeactive,
    DateTime CreateDate);