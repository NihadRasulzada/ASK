using App.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace App.BL.DTOs;

/// <summary>
/// Setting dəyərini yeniləmək üçün DTO.
/// Link tipli setting üçün PdfFile, Text tipli üçün Value istifadə olunur.
/// </summary>
public class UpdateSettingDto
{
    /// <summary>Link tipli setting üçün PDF fayl (application/pdf, max 10 MB).</summary>
    public IFormFile? PdfFile { get; set; }

    /// <summary>Text tipli setting üçün dəyər.</summary>
    public string? Value { get; set; }
}

/// <summary>
/// Client-ə qaytarılan Setting məlumatı.
/// </summary>
public record SettingResponseDto(
    Guid Id,
    string Key,
    string DisplayName,
    string? Value,
    SettingValueType ValueType);
