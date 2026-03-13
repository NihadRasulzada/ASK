using App.BL.DTOs;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.Gallery;

public interface IGalleryService
{
    Task<Response<IEnumerable<GalleryResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Response<GalleryResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response> CreateAsync(CreateGalleryDto dto, CancellationToken cancellationToken = default);
    Task<Response<GalleryResponseDto?>> UpdateAsync(Guid id, UpdateGalleryDto dto, CancellationToken cancellationToken = default);
    Task<Response> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
