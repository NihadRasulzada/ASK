using App.BL.DTOs;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.UsefulLink;

public interface IUsefulLinkService
{
    Task<Response<IEnumerable<UsefulLinkResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Response<IEnumerable<UsefulLinkResponseDto>>> GetAllIncludingDeletedAsync(CancellationToken cancellationToken = default);
    Task<Response<UsefulLinkResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response<UsefulLinkResponseDto>> CreateAsync(CreateUsefulLinkDto dto, CancellationToken cancellationToken = default);
    Task<Response<UsefulLinkResponseDto?>> UpdateAsync(Guid id, UpdateUsefulLinkDto dto, CancellationToken cancellationToken = default);
    Task<Response> ActivateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response> DeActivateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
