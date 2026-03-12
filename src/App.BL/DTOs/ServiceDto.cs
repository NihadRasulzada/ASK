using Microsoft.AspNetCore.Http;

namespace App.BL.DTOs;

public record CreateServiceDto
{
    public IFormFile Image { get; set; } = null!;
    public string NameAz { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string NameRu { get; set; } = string.Empty;
}

public record UpdateServiceDto
{
    public Guid Id { get; set; }
    public IFormFile? Image { get; set; }
    public string NameAz { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string NameRu { get; set; } = string.Empty;
}

public record ServiceResponseDto(Guid Id, string ImageUrl, string Name, bool IsDeactive);
