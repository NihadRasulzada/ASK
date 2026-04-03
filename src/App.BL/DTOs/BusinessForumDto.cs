using Microsoft.AspNetCore.Http;

namespace App.BL.DTOs;

public class CreateBusinessForumDto
{
    public string TitleAz { get; set; }
    public string TitleEn { get; set; }
    public string TitleRu { get; set; }
    public string TextAz { get; set; }
    public string TextEn { get; set; }
    public string TextRu { get; set; }
    public IFormFile TitleImage { get; set; }
    public IFormFile DetailImage { get; set; }
}

public class UpdateBusinessForumDto
{
    public string TitleAz { get; set; }
    public string TitleEn { get; set; }
    public string TitleRu { get; set; }
    public string TextAz { get; set; }
    public string TextEn { get; set; }
    public string TextRu { get; set; }
    public IFormFile? TitleImage { get; set; }
    public IFormFile? DetailImage { get; set; }
}

public record BusinessForumResponseDto(
    Guid Id,
    string Title,
    string Text,
    string TitleImageUrl,
    string DetailImageUrl,
    DateTime CreateDate);
