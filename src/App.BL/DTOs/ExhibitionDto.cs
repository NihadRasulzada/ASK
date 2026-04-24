using Microsoft.AspNetCore.Http;

namespace App.BL.DTOs;

// FIX: IFormFile saxladığı üçün class (record deyil)
public class CreateExhibitionDto
{
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string TitleAz { get; set; } = string.Empty;
    public string TitleEn { get; set; } = string.Empty;
    public string TitleRu { get; set; } = string.Empty;
    public string TextAz { get; set; } = string.Empty;
    public string TextEn { get; set; } = string.Empty;
    public string TextRu { get; set; } = string.Empty;
    public IFormFile Image { get; set; } = null!;
}

public class UpdateExhibitionDto
{
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string TitleAz { get; set; } = string.Empty;
    public string TitleEn { get; set; } = string.Empty;
    public string TitleRu { get; set; } = string.Empty;
    public string TextAz { get; set; } = string.Empty;
    public string TextEn { get; set; } = string.Empty;
    public string TextRu { get; set; } = string.Empty;
    public IFormFile? Image { get; set; }
}

public record ExhibitionResponseDto(Guid Id, string Title, string Text, string TitleImageUrl, bool IsDeactive, DateTime Created);