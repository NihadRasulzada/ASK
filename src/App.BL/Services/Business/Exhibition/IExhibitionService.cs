using App.BL.DTOs;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.Exhibition;

public interface IExhibitionService
{
    Task<Response<IEnumerable<ExhibitionResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Response<IEnumerable<ExhibitionResponseDto>>> GetAllIncludingDeletedAsync(CancellationToken cancellationToken = default);
    Task<Response<ExhibitionResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response<ExhibitionResponseDto>> CreateAsync(CreateExhibitionDto dto, CancellationToken cancellationToken = default);
    Task<Response<ExhibitionResponseDto?>> UpdateAsync(Guid id, UpdateExhibitionDto dto, CancellationToken cancellationToken = default);
    Task<Response> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response> ActivateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response> DeActivateAsync(Guid id, CancellationToken cancellationToken = default);
}
