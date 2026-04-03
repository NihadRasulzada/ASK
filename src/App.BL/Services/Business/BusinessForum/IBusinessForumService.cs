using App.BL.DTOs;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.BusinessForum;

public interface IBusinessForumService
{
    Task<PagedResponse<IEnumerable<BusinessForumResponseDto>>> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);
    Task<Response<BusinessForumResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response<BusinessForumResponseDto>> CreateAsync(CreateBusinessForumDto dto, CancellationToken cancellationToken = default);
    Task<Response<BusinessForumResponseDto?>> UpdateAsync(Guid id, UpdateBusinessForumDto dto, CancellationToken cancellationToken = default);
    Task<Response> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
