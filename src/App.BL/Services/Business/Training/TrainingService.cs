using App.BL.DTOs;
using App.BL.Mapper.Training;
using App.BL.Services.External;
using App.Core.Interfaces.Repository.Training;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.Training;

public class TrainingService(
    ITrainingReadRepository readRepository,
    ITrainingWriteRepository writeRepository,
    ICloudinaryService cloudinaryService,
    ITrainingMapper mapper) : ITrainingService
{
    public async Task<Response> ActivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.Training? entity = await readRepository.GetByIdIncludingDeletedAsync(id, true, cancellationToken);
        if (entity == null) return Response.NotFound("Training not found");
        if (!entity.IsDeactive) return Response.BadRequest("Training is already active");

        entity.Activate();
        await writeRepository.SaveChangesAsync(cancellationToken);
        return Response.Success("Training activated successfully");
    }

    public async Task<Response<TrainingResponseDto>> CreateAsync(CreateTrainingDto dto, CancellationToken cancellationToken = default)
    {
        string imageUrl = await cloudinaryService.UploadImageAsync(dto.Image);
        Core.Entities.Training entity = mapper.CreateDtoToDomain(dto, imageUrl);

        await writeRepository.AddAsync(entity, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        TrainingResponseDto response = mapper.DomainToResponseDto(entity);
        return Response<TrainingResponseDto>.Success(response, "Training created successfully");
    }

    public async Task<Response> DeActivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.Training? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);
        if (entity == null) return Response.NotFound("Training not found");
        if (entity.IsDeactive) return Response.BadRequest("Training is already deactive");

        entity.Deactivate();
        await writeRepository.SaveChangesAsync(cancellationToken);
        return Response.Success("Training deactivated successfully");
    }

    public async Task<Response> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.Training? entity = await readRepository.GetByIdIncludingDeletedAsync(id, true, cancellationToken);
        if (entity == null) return Response.NotFound("Training not found");

        await writeRepository.HardDeleteAsync(id, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);
        return Response.Success("Training deleted successfully");
    }

    public async Task<PagedResponse<IEnumerable<TrainingResponseDto>>> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
    {
        var (items, totalCount) = await readRepository.GetPagedAsync(false, false, pageIndex, pageSize, cancellationToken);
        var result = items.Select(mapper.DomainToResponseDto);
        return PagedResponse<IEnumerable<TrainingResponseDto>>.Create(result, pageIndex, pageSize, totalCount, "Trainings retrieved successfully");
    }

    public async Task<PagedResponse<IEnumerable<TrainingResponseDto>>> GetAllIncludingDeletedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
    {
        var (items, totalCount) = await readRepository.GetPagedAsync(false, true, pageIndex, pageSize, cancellationToken);
        var result = items.Select(mapper.DomainToResponseDto);
        return PagedResponse<IEnumerable<TrainingResponseDto>>.Create(result, pageIndex, pageSize, totalCount, "All trainings retrieved successfully");
    }

    public async Task<Response<TrainingResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.Training? entity = await readRepository.GetByIdIncludingDeletedAsync(id, false, cancellationToken);
        if (entity == null) return Response<TrainingResponseDto?>.NotFound("Training not found");

        TrainingResponseDto dto = mapper.DomainToResponseDto(entity);
        return Response<TrainingResponseDto?>.Success(dto, "Training retrieved successfully");
    }

    public async Task<Response<TrainingResponseDto?>> UpdateAsync(Guid id, UpdateTrainingDto dto, CancellationToken cancellationToken = default)
    {
        Core.Entities.Training? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);
        if (entity == null) return Response<TrainingResponseDto?>.NotFound("Training not found");

        string? newImageUrl = null;
        if (dto.Image != null)
        {
            newImageUrl = await cloudinaryService.UploadImageAsync(dto.Image);
        }

        mapper.UpdateDtoToDomain(entity, dto, newImageUrl);
        writeRepository.Update(entity);
        await writeRepository.SaveChangesAsync(cancellationToken);

        TrainingResponseDto response = mapper.DomainToResponseDto(entity);
        return Response<TrainingResponseDto?>.Success(response, "Training updated successfully");
    }
}
