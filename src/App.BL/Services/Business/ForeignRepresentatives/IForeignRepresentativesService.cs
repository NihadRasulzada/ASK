using App.BL.DTOs;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.ForeignRepresentatives;

public interface IForeignRepresentativesService
{
    Task<Response<IEnumerable<ForeignRepresentativesResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Response<ForeignRepresentativesResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response<ForeignRepresentativesResponseDto>> CreateAsync(CreateForeignRepresentativesDto dto, CancellationToken cancellationToken = default);
    Task<Response<ForeignRepresentativesResponseDto?>> UpdateAsync(Guid id, UpdateForeignRepresentativesDto dto, CancellationToken cancellationToken = default);
    Task<Response<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
