using Microsoft.AspNetCore.Http;

namespace App.BL.DTOs;

/// <summary>
/// Xəbər yaratmaq üçün istifadə olunan DTO.
/// IFormFile saxladığı üçün class kimi tərif edilib (record deyil).
/// </summary>
public class CreateNewsDto
{
    public IFormFile TitleImage { get; set; } = null!;

    public string NewsTextAz { get; set; } = string.Empty;
    public string NewsTextEn { get; set; } = string.Empty;
    public string NewsTextRu { get; set; } = string.Empty;
}

/// <summary>
/// Xəbər yeniləmək üçün istifadə olunan DTO.
/// </summary>
public class UpdateNewsDto
{
    public IFormFile? TitleImage { get; set; }

    public string NewsTextAz { get; set; } = string.Empty;
    public string NewsTextEn { get; set; } = string.Empty;
    public string NewsTextRu { get; set; } = string.Empty;
}

/// <summary>
/// Client-ə qaytarılan Xəbər məlumatı.
/// </summary>
/// <param name="Id">Xəbərin unikal identifikatoru.</param>
/// <param name="TitleImageUrl">Başlıq şəklinin Cloudinary URL-i.</param>
/// <param name="NewsText">Xəbərin mətni.</param>
/// <param name="ImageUrls">Əlavə şəkillərin URL siyahısı.</param>
/// <param name="CreatedOn">Yaradılma tarixi.</param>
/// <param name="IsActive">Xəbərin aktiv olub-olmadığı.</param>
public record NewsResponseDto(
    Guid Id,
    string TitleImageUrl,
    string NewsText,
    IList<string> ImageUrls,
    bool IsDeactive);
