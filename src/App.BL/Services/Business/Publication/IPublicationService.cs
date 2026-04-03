using App.BL.DTOs;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.Publication;

public interface IPublicationService
{
    Task<PagedResponse<IEnumerable<PublicationResponseDto>>> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);
    Task<Response<PublicationResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response<PublicationResponseDto>> CreateAsync(CreatePublicationDto dto, CancellationToken cancellationToken = default);
    Task<Response<PublicationResponseDto?>> UpdateAsync(Guid id, UpdatePublicationDto dto, CancellationToken cancellationToken = default);
    Task<Response> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
