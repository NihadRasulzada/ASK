namespace App.BL.DTOs;

/// <summary>
/// Video yaratmaq üçün istifadə olunan DTO.
/// </summary>
public record CreateVideoDto(string Link, string Title);

/// <summary>
/// Video yeniləmək üçün istifadə olunan DTO.
/// </summary>
public record UpdateVideoDto(string Link, string Title);

/// <summary>
/// Client-ə qaytarılan Video məlumatı.
/// </summary>
public record VideoResponseDto(Guid Id, string Link, string Title);
