using App.BL.DTOs;

namespace App.BL.Services.Abstractions;

public interface INewsService
{
    Task<IEnumerable<NewsResponseDto>> GetAllAsync();
    Task<NewsResponseDto?> GetByIdAsync(Guid id);
    Task<NewsResponseDto> CreateAsync(CreateNewsDto dto);
    Task<NewsResponseDto?> UpdateAsync(Guid id, UpdateNewsDto dto);
    Task<bool> DeleteAsync(Guid id);

    /// <summary>Xəbəri aktiv edir (IsActive = true).</summary>
    Task<bool> ActivateAsync(Guid id);

    /// <summary>Xəbəri deaktiv edir (IsActive = false).</summary>
    Task<bool> DeactivateAsync(Guid id);
}
