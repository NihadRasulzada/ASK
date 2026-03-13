using App.BL.DTOs;
using App.BL.Mapper.Video;
using App.Core.Interfaces.Repository.Video;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.Video;

public class VideoService(
    IVideoReadRepository readRepository,
    IVideoWriteRepository writeRepository,
    IVideoMapper mapper) : IVideoService
{
    public async Task<Response> CreateAsync(CreateVideoDto dto, CancellationToken cancellationToken = default)
    {
        Core.Entities.Video entity = mapper.CreateDtoToDomain(dto);

        await writeRepository.AddAsync(entity, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response.Success("Video created successfully");
    }

    public async Task<Response> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.Video? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
            return Response.NotFound("Video not found");

        await writeRepository.HardDeleteAsync(id, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response.Success("Video deleted successfully");
    }

    public async Task<Response<IEnumerable<VideoResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        // For BaseEntity we use standard GetAllAsync without parameter for soft delete usually,
        // but looking at usage we'll just pass false for tracking. 
        // Note: The Video entity is a BaseEntity, not SoftDeletableEntity. 
        // The repository might just have GetAllAsync(bool tracking).
        IEnumerable<Core.Entities.Video> entities = await readRepository.GetAllAsync(false, cancellationToken);

        if (!entities.Any())
            return Response<IEnumerable<VideoResponseDto>>
                .Success(Enumerable.Empty<VideoResponseDto>(), "No videos found");

        IEnumerable<VideoResponseDto> result = entities.Select(x => mapper.DomainToResponseDto(x));

        return Response<IEnumerable<VideoResponseDto>>
            .Success(result, $"{result.Count()} videos retrieved successfully");
    }

    public async Task<Response<VideoResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.Video? entity = await readRepository.GetByIdAsync(id, false, cancellationToken);

        if (entity == null)
            return Response<VideoResponseDto?>.NotFound("Video not found");

        VideoResponseDto dto = mapper.DomainToResponseDto(entity);

        return Response<VideoResponseDto?>.Success(dto, "Video retrieved successfully");
    }

    public async Task<Response<VideoResponseDto?>> UpdateAsync(Guid id, UpdateVideoDto dto, CancellationToken cancellationToken = default)
    {
        Core.Entities.Video? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
            return Response<VideoResponseDto?>.NotFound("Video not found");

        mapper.UpdateDtoToDomain(entity, dto);

        writeRepository.Update(entity);
        await writeRepository.SaveChangesAsync(cancellationToken);

        VideoResponseDto response = mapper.DomainToResponseDto(entity);

        return Response<VideoResponseDto?>.Success(response, "Video updated successfully");
    }
}
