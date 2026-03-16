using Microsoft.AspNetCore.Http;

namespace App.BL.DTOs;

// FIX: IFormFile saxladığı üçün class (record deyil)
public class CreateGalleryDto
{
    public IFormFile Image { get; set; } = null!;
}

public class UpdateGalleryDto
{
    public IFormFile? Image { get; set; }
}

public record GalleryResponseDto(Guid Id, string ImageUrl);