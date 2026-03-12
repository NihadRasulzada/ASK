using App.BL.DTOs;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.Director;

public interface IDirectorService
{
    Task<Response<IEnumerable<DirectorResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Response<DirectorResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response<DirectorResponseDto>> CreateAsync(CreateDirectorDto dto, CancellationToken cancellationToken = default);
    Task<Response<DirectorResponseDto?>> UpdateAsync(Guid id, UpdateDirectorDto dto, CancellationToken cancellationToken = default);
    Task<Response<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response<IEnumerable<DirectorResponseDto>>> GetAllIncludingDeletedAsync(CancellationToken cancellationToken = default);
    Task<Response> ActivateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response> DeActivateAsync(Guid id, CancellationToken cancellationToken = default);
}