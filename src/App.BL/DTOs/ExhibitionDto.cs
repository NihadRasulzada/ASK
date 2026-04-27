using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;

namespace App.BL.DTOs;

// FIX: IFormFile saxladığı üçün class (record deyil)
public class CreateExhibitionDto
{
    public string Text { get; set; } = string.Empty;
    public string TitleAz { get; set; } = string.Empty;
    public string TitleEn { get; set; } = string.Empty;
    public string TitleRu { get; set; } = string.Empty;
    public string TextAz { get; set; } = string.Empty;
    public string TextEn { get; set; } = string.Empty;
    public string TextRu { get; set; } = string.Empty;
    public IFormFile Image { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class UpdateExhibitionDto
{
    public string Text { get; set; } = string.Empty;
    public string TitleAz { get; set; } = string.Empty;
    public string TitleEn { get; set; } = string.Empty;
    public string TitleRu { get; set; } = string.Empty;
    public string TextAz { get; set; } = string.Empty;
    public string TextEn { get; set; } = string.Empty;
    public string TextRu { get; set; } = string.Empty;
    public IFormFile? Image { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public record ExhibitionResponseDto(Guid Id, string Title, string Text, string TitleImageUrl, bool IsDeactive, DateTime Created, DateTime StartDate, DateTime EndDate);