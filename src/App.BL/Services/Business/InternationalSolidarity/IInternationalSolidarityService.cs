using App.BL.DTOs;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.InternationalSolidarity;

public interface IInternationalSolidarityService
{
    Task<Response<IEnumerable<InternationalSolidarityResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Response<InternationalSolidarityResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response<InternationalSolidarityResponseDto>> CreateAsync(CreateInternationalSolidarityDto dto, CancellationToken cancellationToken = default);
    Task<Response<InternationalSolidarityResponseDto?>> UpdateAsync(Guid id, UpdateInternationalSolidarityDto dto, CancellationToken cancellationToken = default);
    Task<Response<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
