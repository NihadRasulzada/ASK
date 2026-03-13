using App.BL.DTOs;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.Training;

public interface ITrainingService
{
    Task<Response<IEnumerable<TrainingResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Response<IEnumerable<TrainingResponseDto>>> GetAllIncludingDeletedAsync(CancellationToken cancellationToken = default);
    Task<Response<TrainingResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response<TrainingResponseDto>> CreateAsync(CreateTrainingDto dto, CancellationToken cancellationToken = default);
    Task<Response<TrainingResponseDto?>> UpdateAsync(Guid id, UpdateTrainingDto dto, CancellationToken cancellationToken = default);
    Task<Response> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response> ActivateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response> DeActivateAsync(Guid id, CancellationToken cancellationToken = default);
}
