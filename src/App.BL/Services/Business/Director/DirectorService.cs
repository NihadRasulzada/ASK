using App.BL.DTOs;
using App.BL.Mapper.Director;
using App.BL.Services.External;
using App.Core.Interfaces.Repository.Director;
using App.Core.ResponseObject.Concreate;
using App.DAL.Context;

namespace App.BL.Services.Business.Director;

public class DirectorService(
    IDirectorReadRepository readRepository,
    IDirectorWriteRepository writeRepository,
    ICloudinaryService cloudinaryService,
    IDirectorMapper directorMapper,
    AppDbContext context) : IDirectorService
{

    public async Task<Response> ActivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.Director? entity = await readRepository.GetByIdIncludingDeletedAsync(id, true, cancellationToken);

        if (entity == null)
            return Response.NotFound("Director not found");

        if (!entity.IsDeactive)
            return Response.BadRequest("Director is already active");

        entity.Activate();

        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response.Success("Director activated successfully");
    }

    public async Task<Response<DirectorResponseDto>> CreateAsync(CreateDirectorDto dto, CancellationToken cancellationToken = default)
    {
        string imageUrl = await cloudinaryService.UploadImageAsync(dto.Image);

        Core.Entities.Director entity = directorMapper.CreateDtoToDomain(dto, imageUrl);

        await writeRepository.AddAsync(entity, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        DirectorResponseDto response = directorMapper.DomainToResponseDto(entity);

        return Response<DirectorResponseDto>.Success(response, "Director created successfully");
    }

    public async Task<Response> DeActivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.Director? entity = await readRepository.GetByIdIncludingDeletedAsync(id, true, cancellationToken);

        if (entity == null)
            return Response.NotFound("Director not found");

        if (entity.IsDeactive)
            return Response.BadRequest("Director is already deactive");

        entity.Deactivate();

        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response.Success("Director deactivated successfully");
    }

    public async Task<Response<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.Director? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
            return Response<bool>.NotFound("Director not found");

        await writeRepository.HardDeleteAsync(id, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<bool>.Success(true, "Director deleted successfully");
    }

    public async Task<Response<IEnumerable<DirectorResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<Core.Entities.Director> entities = await readRepository.GetAllAsync(false, cancellationToken, false);

        if (!entities.Any())
            return Response<IEnumerable<DirectorResponseDto>>
                .Success(Enumerable.Empty<DirectorResponseDto>(), "No directors found");

        IEnumerable<DirectorResponseDto> result = entities.Select(x => directorMapper.DomainToResponseDto(x));

        return Response<IEnumerable<DirectorResponseDto>>
            .Success(result, $"{result.Count()} directors retrieved successfully");
    }

    public async Task<Response<IEnumerable<DirectorResponseDto>>> GetAllIncludingDeletedAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<Core.Entities.Director> entities = await readRepository.GetAllIncludingDeletedAsync(cancellationToken);

        if (!entities.Any())
            return Response<IEnumerable<DirectorResponseDto>>
                .Success(Enumerable.Empty<DirectorResponseDto>(), "No directors found");

        IEnumerable<DirectorResponseDto> result = entities.Select(x => directorMapper.DomainToResponseDto(x));

        return Response<IEnumerable<DirectorResponseDto>>
            .Success(result, $"{result.Count()} directors retrieved successfully");
    }

    public async Task<Response<DirectorResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.Director? entity = await readRepository.GetByIdAsync(id, false, cancellationToken);

        if (entity == null)
            return Response<DirectorResponseDto?>.NotFound("Director not found");

        DirectorResponseDto dto = directorMapper.DomainToResponseDto(entity);

        return Response<DirectorResponseDto?>.Success(dto, "Director retrieved successfully");
    }

    public async Task<Response<DirectorResponseDto?>> UpdateAsync(Guid id, UpdateDirectorDto dto, CancellationToken cancellationToken = default)
    {
        Core.Entities.Director? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
            return Response<DirectorResponseDto?>.NotFound("Director not found");

        string imageUrl = entity.ImageUrl;

        if (dto.Image != null)
        {
            imageUrl = await cloudinaryService.UploadImageAsync(dto.Image);
        }

        directorMapper.UpdateDtoToDamain(entity, dto, imageUrl);

        writeRepository.Update(entity);
        await writeRepository.SaveChangesAsync(cancellationToken);

        DirectorResponseDto response = directorMapper.DomainToResponseDto(entity);

        return Response<DirectorResponseDto?>.Success(response, "Director updated successfully");
    }
}