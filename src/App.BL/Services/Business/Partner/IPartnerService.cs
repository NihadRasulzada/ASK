using App.BL.DTOs;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.Partner;

public interface IPartnerService
{
    Task<Response<IEnumerable<PartnerResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Response<PartnerResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response> CreateAsync(CreatePartnerDto dto, CancellationToken cancellationToken = default);
    Task<Response<PartnerResponseDto?>> UpdateAsync(Guid id, UpdatePartnerDto dto, CancellationToken cancellationToken = default);
    Task<Response> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
