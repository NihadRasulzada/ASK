using App.BL.DTOs;

namespace App.BL.Services.Abstractions;

public interface IVideoService
{
    Task<IEnumerable<VideoResponseDto>> GetAllAsync();
    Task<VideoResponseDto?> GetByIdAsync(Guid id);
    Task<VideoResponseDto> CreateAsync(CreateVideoDto dto);
    Task<VideoResponseDto?> UpdateAsync(Guid id, UpdateVideoDto dto);
    Task<bool> DeleteAsync(Guid id);
}
