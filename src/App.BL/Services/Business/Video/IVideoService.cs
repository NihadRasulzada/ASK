using App.BL.DTOs;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.Video;

public interface IVideoService
{
    Task<Response<IEnumerable<VideoResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Response<VideoResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response> CreateAsync(CreateVideoDto dto, CancellationToken cancellationToken = default);
    Task<Response<VideoResponseDto?>> UpdateAsync(Guid id, UpdateVideoDto dto, CancellationToken cancellationToken = default);
    Task<Response> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
