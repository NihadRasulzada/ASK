using App.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace App.BL.DTOs;

/// <summary>
/// Setting dəyərini yeniləmək üçün DTO.
/// Link tipli setting üçün PdfFile, Text tipli üçün Value istifadə olunur.
/// </summary>
public class UpdateSettingDto
{
    /// <summary>Link tipli setting üçün fayl (şəkil və ya PDF).</summary>
    public IFormFile? File { get; set; }

    /// <summary>Text tipli setting üçün dəyər.</summary>
    public string? Value { get; set; }
}

/// <summary>
/// Client-ə qaytarılan Setting məlumatı.
/// </summary>
public record SettingResponseDto(
    Guid Id,
    string Key,
    string? StringValue,
    string? CloudinaryUrl,   
    SettingValueType ValueType);
