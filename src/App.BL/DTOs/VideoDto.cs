namespace App.BL.DTOs;

/// <summary>
/// Video yaratmaq üçün istifadə olunan DTO.
/// </summary>
/// <param name="Link">Videonun URL linki.</param>
public record CreateVideoDto(string Link);

/// <summary>
/// Video yeniləmək üçün istifadə olunan DTO.
/// </summary>
/// <param name="Link">Yeni URL link.</param>
public record UpdateVideoDto(string Link);

/// <summary>
/// Client-ə qaytarılan Video məlumatı.
/// </summary>
/// <param name="Id">Videonun unikal identifikatoru.</param>
/// <param name="Link">Videonun URL linki.</param>
public record VideoResponseDto(Guid Id, string Link);
