using Microsoft.AspNetCore.Http;

namespace App.BL.DTOs;

public record CreateDirectorDto
{
    public IFormFile Image { get; set; } = null!;
    public string FullNameAz { get; set; } = string.Empty;
    public string FullNameEn { get; set; } = string.Empty;
    public string FullNameRu { get; set; } = string.Empty;
    public string DutyAz { get; set; } = string.Empty;
    public string DutyEn { get; set; } = string.Empty;
    public string DutyRu { get; set; } = string.Empty;
}

public record UpdateDirectorDto
{
    public IFormFile? Image { get; set; }
    public string FullNameAz { get; set; } = string.Empty;
    public string FullNameEn { get; set; } = string.Empty;
    public string FullNameRu { get; set; } = string.Empty;
    public string DutyAz { get; set; } = string.Empty;
    public string DutyEn { get; set; } = string.Empty;
    public string DutyRu { get; set; } = string.Empty;
}

public record DirectorResponseDto(Guid Id, string ImageUrl, string FullName, string Duty, bool IsDeactive);
