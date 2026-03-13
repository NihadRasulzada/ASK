using App.BL.DTOs;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.Announcement;

public interface IAnnouncementService
{
    Task<Response<IEnumerable<AnnouncementResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Response<AnnouncementResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response> CreateAsync(CreateAnnouncementDto dto, CancellationToken cancellationToken = default);
    Task<Response<AnnouncementResponseDto?>> UpdateAsync(Guid id, UpdateAnnouncementDto dto, CancellationToken cancellationToken = default);
    Task<Response> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
