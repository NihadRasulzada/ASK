using Microsoft.AspNetCore.Http;

namespace App.BL.DTOs;

/// <summary>
/// Gallery yaratmaq üçün istifadə olunan DTO.
/// </summary>
/// <param name="Image">Yüklənəcək şəkil faylı.</param>
public record CreateGalleryDto(IFormFile Image);

/// <summary>
/// Gallery yeniləmək üçün istifadə olunan DTO.
/// </summary>
/// <param name="Image">Yeni şəkil faylı (istəyə bağlı).</param>
public record UpdateGalleryDto(IFormFile? Image);

/// <summary>
/// Client-ə qaytarılan Gallery məlumatı.
/// </summary>
/// <param name="Id">Qalereya şəklinin unikal identifikatoru.</param>
/// <param name="ImageUrl">Şəklin URL linki.</param>
public record GalleryResponseDto(Guid Id, string ImageUrl);
