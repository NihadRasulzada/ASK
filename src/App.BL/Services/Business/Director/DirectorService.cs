using App.BL.DTOs;
using App.BL.Mapper.Director;
using App.BL.Services.External;
using App.Core.Entities.Common.Storage;
using App.Core.Interfaces.Repository.Director;
using App.Core.ResponseObject.Concreate;
using App.DAL.Context;
using static System.Net.Mime.MediaTypeNames;

namespace App.BL.Services.Business.Director;

public class DirectorService(
    IDirectorReadRepository readRepository,
    IDirectorWriteRepository writeRepository,
    IStorageService storageService,
    IDirectorMapper directorMapper,
    AppDbContext context) : StorageEntityService(storageService), IDirectorService
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
        StoredFile imageUrl = await storageService.UploadAsync(dto.Image);

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

        await DeleteFileAsync(entity.ImageUrl.ObjectKey);

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

        StoredFile imageUrl = entity.ImageUrl;

        if (dto.Image != null)
        {
            var (newUrl, oldPublicId) = await ReplaceFileAsync(  
                entity.ImageUrl.ObjectKey,
                dto.Image);

            directorMapper.UpdateDtoToDamain(entity, dto, newUrl);
            await DeleteFileAsync(oldPublicId); 
        }
        else
        {
            directorMapper.UpdateDtoToDamain(entity, dto, null);
        }

        writeRepository.Update(entity);
        await writeRepository.SaveChangesAsync(cancellationToken);

        DirectorResponseDto response = directorMapper.DomainToResponseDto(entity);

        return Response<DirectorResponseDto?>.Success(response, "Director updated successfully");
    }
}