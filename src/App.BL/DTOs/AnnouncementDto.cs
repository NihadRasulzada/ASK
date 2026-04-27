using Microsoft.AspNetCore.Http;

namespace App.BL.DTOs;

// FIX: IFormFile saxladığı üçün class (record deyil)
// FIX: UpdateAnnouncementDto-dan Guid Id silindi — route-dan gəlməlidir
public class CreateAnnouncementDto
{
    public string TitleAz { get; set; } = string.Empty;
    public string TitleEn { get; set; } = string.Empty;
    public string TitleRu { get; set; } = string.Empty;
    public IFormFile TitleImage { get; set; } = null!;
    public string TextAz { get; set; } = string.Empty;
    public string TextEn { get; set; } = string.Empty;
    public string TextRu { get; set; } = string.Empty;
}

public class UpdateAnnouncementDto
{
    public string TitleAz { get; set; } = string.Empty;
    public string TitleEn { get; set; } = string.Empty;
    public string TitleRu { get; set; } = string.Empty;
    public IFormFile? TitleImage { get; set; }
    public string TextAz { get; set; } = string.Empty;
    public string TextEn { get; set; } = string.Empty;
    public string TextRu { get; set; } = string.Empty;
}

public record AnnouncementResponseDto(Guid Id, string Title, string TitleImageUrl, string Text , DateTime Created);