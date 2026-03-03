using Microsoft.AspNetCore.Http;

namespace App.BL.DTOs;

/// <summary>
/// Partner yaratmaq üçün istifadə olunan DTO.
/// IFormFile saxladığı üçün class kimi tərif edilib (record deyil).
/// </summary>
public class CreatePartnerDto
{
    /// <summary>Partnerin şəkli.</summary>
    public IFormFile Image { get; set; } = null!;

    /// <summary>Partnerin sayt URL-i.</summary>
    public string Site { get; set; } = string.Empty;
}

/// <summary>
/// Partner yeniləmək üçün istifadə olunan DTO.
/// </summary>
public class UpdatePartnerDto
{
    /// <summary>Yeni şəkil; null olarsa mövcud şəkil URL-i saxlanır.</summary>
    public IFormFile? Image { get; set; }

    /// <summary>Yeni sayt URL-i.</summary>
    public string Site { get; set; } = string.Empty;
}

/// <summary>
/// Client-ə qaytarılan Partner məlumatı.
/// </summary>
/// <param name="Id">Partnerin unikal identifikatoru.</param>
/// <param name="ImageUrl">Cloudinary-dəki şəkil URL-i.</param>
/// <param name="Site">Partnerin sayt URL-i.</param>
public record PartnerResponseDto(Guid Id, string ImageUrl, string Site);
