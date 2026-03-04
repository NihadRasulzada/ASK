using App.BL.DTOs;

namespace App.BL.Services.Abstractions;

public interface IDirectorService
{
    Task<IEnumerable<DirectorResponseDto>> GetAllAsync();
    Task<DirectorResponseDto?> GetByIdAsync(Guid id);
    Task<DirectorResponseDto> CreateAsync(CreateDirectorDto dto);
    Task<DirectorResponseDto?> UpdateAsync(Guid id, UpdateDirectorDto dto);
    Task<bool> DeleteAsync(Guid id);
}
