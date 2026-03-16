using App.BL.DTOs;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.Service;

public interface IServiceService
{
    Task<Response<IEnumerable<ServiceResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Response<ServiceResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response> CreateAsync(CreateServiceDto dto, CancellationToken cancellationToken = default);
    Task<Response<ServiceResponseDto?>> UpdateAsync(Guid id,UpdateServiceDto dto, CancellationToken cancellationToken = default);
    Task<Response> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response<IEnumerable<ServiceResponseDto>>> GetAllIncludingDeletedAsync(CancellationToken cancellationToken = default);
    Task<Response> ActivateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response> DeActivateAsync(Guid id, CancellationToken cancellationToken = default);
}