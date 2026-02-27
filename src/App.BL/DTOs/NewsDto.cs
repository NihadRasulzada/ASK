using Microsoft.AspNetCore.Http;

namespace App.BL.DTOs;

/// <summary>
/// Xəbər yaratmaq üçün istifadə olunan DTO.
/// IFormFile saxladığı üçün class kimi tərif edilib (record deyil).
/// </summary>
public class CreateNewsDto
{
    /// <summary>Xəbərin başlıq şəkli (məcburi).</summary>
    public IFormFile TitleImage { get; set; } = null!;

    /// <summary>Xəbərin mətni.</summary>
    public string NewsText { get; set; } = string.Empty;

    /// <summary>Əlavə şəkillər (könüllü).</summary>
    public List<IFormFile>? AdditionalImages { get; set; }
}

/// <summary>
/// Xəbər yeniləmək üçün istifadə olunan DTO.
/// </summary>
public class UpdateNewsDto
{
    /// <summary>Yeni başlıq şəkli; null olarsa mövcud şəkil URL-i saxlanır.</summary>
    public IFormFile? TitleImage { get; set; }

    /// <summary>Yeni xəbər mətni.</summary>
    public string NewsText { get; set; } = string.Empty;

    /// <summary>Yeni əlavə şəkillər; null olarsa mövcud URL siyahısı saxlanır.</summary>
    public List<IFormFile>? AdditionalImages { get; set; }
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
    DateTimeOffset CreatedOn,
    bool IsActive);
