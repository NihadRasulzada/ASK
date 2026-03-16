using Microsoft.AspNetCore.Http;

namespace App.BL.DTOs;

// FIX: IFormFile saxladığı üçün class (record deyil)
// FIX: UpdateAnnouncementDto-dan Guid Id silindi — route-dan gəlməlidir
public class CreateAnnouncementDto
{
    public string Title { get; set; } = string.Empty;
    public IFormFile TitleImage { get; set; } = null!;
    public string Text { get; set; } = string.Empty;
}

public class UpdateAnnouncementDto
{
    public string Title { get; set; } = string.Empty;
    public IFormFile? TitleImage { get; set; }
    public string Text { get; set; } = string.Empty;
}

public record AnnouncementResponseDto(Guid Id, string Title, string TitleImageUrl, string Text);