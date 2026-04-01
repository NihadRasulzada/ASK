using Microsoft.AspNetCore.Http;

namespace App.BL.DTOs;

public class CreatePresidiumDto
{
    public string FullNameAz { get; set; } = string.Empty;
    public string FullNameEn { get; set; } = string.Empty;
    public string FullNameRu { get; set; } = string.Empty;
    public string PositionAz { get; set; } = string.Empty;
    public string PositionEn { get; set; } = string.Empty;
    public string PositionRu { get; set; } = string.Empty;
    public IFormFile Image { get; set; } = null!;
}

public class UpdatePresidiumDto
{
    public string FullNameAz { get; set; } = string.Empty;
    public string FullNameEn { get; set; } = string.Empty;
    public string FullNameRu { get; set; } = string.Empty;
    public string PositionAz { get; set; } = string.Empty;
    public string PositionEn { get; set; } = string.Empty;
    public string PositionRu { get; set; } = string.Empty;
    public IFormFile? Image { get; set; }
}

public record PresidiumResponseDto(
    Guid Id,
    string FullName,
    string Position,
    string ImageUrl);
