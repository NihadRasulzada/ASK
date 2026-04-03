using Microsoft.AspNetCore.Http;

namespace App.BL.DTOs;

public class CreatePublicationDto
{
    public string TitleAz { get; set; }
    public string TitleEn { get; set; }
    public string TitleRu { get; set; }
    public IFormFile TitleImage { get; set; }
    public IFormFile PdfFile { get; set; }
}

public class UpdatePublicationDto
{
    public string TitleAz { get; set; }
    public string TitleEn { get; set; }
    public string TitleRu { get; set; }
    public IFormFile? TitleImage { get; set; }
    public IFormFile? PdfFile { get; set; }
}

public record PublicationResponseDto(Guid Id, string Title, string TitleImageUrl, string PdfUrl);
