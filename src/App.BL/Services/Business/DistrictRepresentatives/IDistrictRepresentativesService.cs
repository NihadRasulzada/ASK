using App.BL.DTOs;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.DistrictRepresentatives;

public interface IDistrictRepresentativesService
{
    Task<Response<IEnumerable<DistrictRepresentativesResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Response<DistrictRepresentativesResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response<DistrictRepresentativesResponseDto>> CreateAsync(CreateDistrictRepresentativesDto dto, CancellationToken cancellationToken = default);
    Task<Response<DistrictRepresentativesResponseDto?>> UpdateAsync(Guid id, UpdateDistrictRepresentativesDto dto, CancellationToken cancellationToken = default);
    Task<Response<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
