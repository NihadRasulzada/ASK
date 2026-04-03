using App.BL.DTOs;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.Training;

public interface ITrainingService
{
    Task<PagedResponse<IEnumerable<TrainingResponseDto>>> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);
    Task<PagedResponse<IEnumerable<TrainingResponseDto>>> GetAllIncludingDeletedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);
    Task<Response<TrainingResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response<TrainingResponseDto>> CreateAsync(CreateTrainingDto dto, CancellationToken cancellationToken = default);
    Task<Response<TrainingResponseDto?>> UpdateAsync(Guid id, UpdateTrainingDto dto, CancellationToken cancellationToken = default);
    Task<Response> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response> ActivateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response> DeActivateAsync(Guid id, CancellationToken cancellationToken = default);
}
