using Microsoft.AspNetCore.Http;

namespace App.BL.DTOs;

public class CreateInternationalSolidarityDto
{
    public string Link { get; set; } = string.Empty;
    public IFormFile Icon { get; set; } = null!;
}

public class UpdateInternationalSolidarityDto
{
    public string Link { get; set; } = string.Empty;
    public IFormFile? Icon { get; set; }
}

public record InternationalSolidarityResponseDto(
    Guid Id,
    string Link,
    string IconUrl);
