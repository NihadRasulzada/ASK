using App.BL.DTOs;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.Presidium;

public interface IPresidiumService
{
    Task<Response<IEnumerable<PresidiumResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Response<PresidiumResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response<PresidiumResponseDto>> CreateAsync(CreatePresidiumDto dto, CancellationToken cancellationToken = default);
    Task<Response<PresidiumResponseDto?>> UpdateAsync(Guid id, UpdatePresidiumDto dto, CancellationToken cancellationToken = default);
    Task<Response<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
