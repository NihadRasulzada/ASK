using Microsoft.AspNetCore.Http;

namespace App.BL.DTOs;

// FIX: IFormFile saxlayan DTO record deyil, class olmalıdır
public class CreateServiceDto
{
    public IFormFile Image { get; set; } = null!;
    public string NameAz { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string NameRu { get; set; } = string.Empty;
}

public class UpdateServiceDto
{
    // FIX: Guid Id silindi — route parametrindən gəlməlidir, DTO body-sində olmamalıdır.
    //      Əks halda hem route-dan hem body-dən Id gəlir, ziddiyyət yarana bilər.
    public IFormFile? Image { get; set; }
    public string NameAz { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string NameRu { get; set; } = string.Empty;
}

public record ServiceResponseDto(Guid Id, string ImageUrl, string Name, bool IsDeactive);