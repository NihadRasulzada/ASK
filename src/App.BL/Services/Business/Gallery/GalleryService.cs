using App.BL.DTOs;
using App.BL.Mapper.Gallery;
using App.BL.Services.External;
using App.Core.Entities.Common.Storage;
using App.Core.Interfaces.Repository.Gallery;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.Gallery;

public class GalleryService(
    IGalleryReadRepository readRepository,
    IGalleryWriteRepository writeRepository,
    IStorageService storageService,
    IGalleryMapper mapper) : StorageEntityService(storageService), IGalleryService
{
    public async Task<Response> CreateAsync(CreateGalleryDto dto, CancellationToken cancellationToken = default)
    {
        StoredFile imageUrl = await storageService.UploadAsync(dto.Image);

        Core.Entities.Gallery entity = mapper.CreateDtoToDomain(dto, imageUrl);

        await writeRepository.AddAsync(entity, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response.Success("Gallery image uploaded successfully");
    }

    public async Task<Response> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.Gallery? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
            return Response.NotFound("Gallery image not found");

        await DeleteFileAsync(entity.ImageUrl.ObjectKey);

        await writeRepository.HardDeleteAsync(id, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response.Success("Gallery image deleted successfully");
    }

    public async Task<Response<IEnumerable<GalleryResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<Core.Entities.Gallery> entities = await readRepository.GetAllAsync(false, cancellationToken);

        if (!entities.Any())
            return Response<IEnumerable<GalleryResponseDto>>
                .Success(Enumerable.Empty<GalleryResponseDto>(), "No gallery images found");

        IEnumerable<GalleryResponseDto> result = entities.Select(x => mapper.DomainToResponseDto(x));

        return Response<IEnumerable<GalleryResponseDto>>
            .Success(result, $"{result.Count()} gallery images retrieved successfully");
    }

    public async Task<Response<GalleryResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.Gallery? entity = await readRepository.GetByIdAsync(id, false, cancellationToken);

        if (entity == null)
            return Response<GalleryResponseDto?>.NotFound("Gallery image not found");

        GalleryResponseDto dto = mapper.DomainToResponseDto(entity);

        return Response<GalleryResponseDto?>.Success(dto, "Gallery image retrieved successfully");
    }

    public async Task<Response<GalleryResponseDto?>> UpdateAsync(Guid id, UpdateGalleryDto dto, CancellationToken cancellationToken = default)
    {
        Core.Entities.Gallery? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
            return Response<GalleryResponseDto?>.NotFound("Gallery image not found");

        StoredFile? newImageUrl = null;
        if (dto.Image != null)
        {
            var (newUrl, oldPublicId) = await ReplaceFileAsync(  
                entity.ImageUrl.ObjectKey,
                dto.Image);

            mapper.UpdateDtoToDomain(entity, dto, newUrl);
            await DeleteFileAsync(oldPublicId); 
        }
        else
        {
            mapper.UpdateDtoToDomain(entity, dto, null);
        }

        writeRepository.Update(entity);
        await writeRepository.SaveChangesAsync(cancellationToken);

        GalleryResponseDto response = mapper.DomainToResponseDto(entity);

        return Response<GalleryResponseDto?>.Success(response, "Gallery image updated successfully");
    }
}
