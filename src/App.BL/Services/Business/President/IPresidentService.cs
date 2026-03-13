using App.BL.DTOs;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.President;

public interface IPresidentService
{
    Task<Response<IEnumerable<PresidentResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Response<PresidentResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response> CreateAsync(CreatePresidentDto dto, CancellationToken cancellationToken = default);
    Task<Response<PresidentResponseDto?>> UpdateAsync(Guid id, UpdatePresidentDto dto, CancellationToken cancellationToken = default);
    Task<Response> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
