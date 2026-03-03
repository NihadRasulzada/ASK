using App.BL.DTOs;

namespace App.BL.Services.Abstractions;

public interface IPresidentService
{
    Task<PresidentResponseDto?> GetAsync();

    /// <summary>
    /// Prezident məlumatı yaradır.
    /// DB-də artıq bir qeyd varsa null qaytarır (409 Conflict).
    /// </summary>
    Task<PresidentResponseDto?> CreateAsync(CreatePresidentDto dto);

    /// <summary>
    /// Mövcud prezident məlumatını yeniləyir.
    /// Qeyd tapılmadıqda null qaytarır (404 Not Found).
    /// </summary>
    Task<PresidentResponseDto?> UpdateAsync(UpdatePresidentDto dto);

    Task<bool> DeleteAsync();
}
