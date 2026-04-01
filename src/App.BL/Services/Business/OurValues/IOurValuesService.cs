using App.BL.DTOs;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.OurValues;

public interface IOurValuesService
{
    Task<Response<IEnumerable<OurValuesResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Response<OurValuesResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response<OurValuesResponseDto>> CreateAsync(CreateOurValuesDto dto, CancellationToken cancellationToken = default);
    Task<Response<OurValuesResponseDto?>> UpdateAsync(Guid id, UpdateOurValuesDto dto, CancellationToken cancellationToken = default);
    Task<Response<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
