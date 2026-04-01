using App.BL.DTOs;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.Committee;

public interface ICommitteeService
{
    Task<Response<IEnumerable<CommitteeResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Response<CommitteeResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response<CommitteeResponseDto>> CreateAsync(CreateCommitteeDto dto, CancellationToken cancellationToken = default);
    Task<Response<CommitteeResponseDto?>> UpdateAsync(Guid id, UpdateCommitteeDto dto, CancellationToken cancellationToken = default);
    Task<Response<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
