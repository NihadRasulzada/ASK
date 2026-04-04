using Microsoft.AspNetCore.Http;

namespace App.BL.DTOs;

// FIX: IFormFile saxlayan DTO record deyil, class olmalıdır.
//      Record-un value semantics-i + IFormFile (reference type stream) uyğunsuzluq yaradır;
//      həmçinin bəzi model binding middleware-ləri class tələb edir.
public class CreateDirectorDto
{
    public IFormFile Image { get; set; } = null!;
    public string FullNameAz { get; set; } = string.Empty;
    public string FullNameEn { get; set; } = string.Empty;
    public string FullNameRu { get; set; } = string.Empty;
    public string DutyAz { get; set; } = string.Empty;
    public string DutyEn { get; set; } = string.Empty;
    public string DutyRu { get; set; } = string.Empty;
    public string? DepartmentAz { get; set; }
    public string? DepartmentEn { get; set; }
    public string? DepartmentRu { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}

public class UpdateDirectorDto
{
    public IFormFile? Image { get; set; }
    public string FullNameAz { get; set; } = string.Empty;
    public string FullNameEn { get; set; } = string.Empty;
    public string FullNameRu { get; set; } = string.Empty;
    public string DutyAz { get; set; } = string.Empty;
    public string DutyEn { get; set; } = string.Empty;
    public string DutyRu { get; set; } = string.Empty;
    public string? DepartmentAz { get; set; }
    public string? DepartmentEn { get; set; }
    public string? DepartmentRu { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}

public record DirectorResponseDto(Guid Id, string ImageUrl, string FullName, string Duty, string? Department, string? PhoneNumber, string? Email, bool IsDeactive);