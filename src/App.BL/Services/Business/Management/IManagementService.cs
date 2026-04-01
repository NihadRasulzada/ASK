using App.BL.DTOs;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.Management;

public interface IManagementService
{
    Task<Response<IEnumerable<ManagementResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Response<ManagementResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response<ManagementResponseDto>> CreateAsync(CreateManagementDto dto, CancellationToken cancellationToken = default);
    Task<Response<ManagementResponseDto?>> UpdateAsync(Guid id, UpdateManagementDto dto, CancellationToken cancellationToken = default);
    Task<Response<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
