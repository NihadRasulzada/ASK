using App.BL.DTOs;
using App.BL.Mapper.OurValues;
using App.BL.Services.External;
using App.Core.Entities.Common.Storage;
using App.Core.Interfaces.Repository.OurValues;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.OurValues;

public class OurValuesService(
    IOurValuesReadRepository readRepository,
    IOurValuesWriteRepository writeRepository,
    IStorageService storageService,
    IOurValuesMapper mapper) : StorageEntityService(storageService), IOurValuesService
{
    public async Task<Response<IEnumerable<OurValuesResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<Core.Entities.OurValues> entities = await readRepository.GetAllAsync(false, cancellationToken);

        IEnumerable<OurValuesResponseDto> result = entities.Select(mapper.DomainToResponseDto);

        return Response<IEnumerable<OurValuesResponseDto>>
            .Success(result, $"{result.Count()} values retrieved successfully");
    }

    public async Task<Response<OurValuesResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.OurValues? entity = await readRepository.GetByIdAsync(id, false, cancellationToken);

        if (entity == null)
            return Response<OurValuesResponseDto?>.NotFound("Value not found");

        return Response<OurValuesResponseDto?>.Success(mapper.DomainToResponseDto(entity), "Value retrieved successfully");
    }

    public async Task<Response<OurValuesResponseDto>> CreateAsync(CreateOurValuesDto dto, CancellationToken cancellationToken = default)
    {
        StoredFile imageUrl = await storageService.UploadAsync(dto.Image);

        Core.Entities.OurValues entity = mapper.CreateDtoToDomain(dto, imageUrl);

        await writeRepository.AddAsync(entity, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<OurValuesResponseDto>.Success(mapper.DomainToResponseDto(entity), "Value created successfully");
    }

    public async Task<Response<OurValuesResponseDto?>> UpdateAsync(Guid id, UpdateOurValuesDto dto, CancellationToken cancellationToken = default)
    {
        Core.Entities.OurValues? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
            return Response<OurValuesResponseDto?>.NotFound("Value not found");

        StoredFile imageUrl = entity.ImageUrl;
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

        return Response<OurValuesResponseDto?>.Success(mapper.DomainToResponseDto(entity), "Value updated successfully");
    }

    public async Task<Response<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.OurValues? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
            return Response<bool>.NotFound("Value not found");

        await DeleteFileAsync(entity.ImageUrl.ObjectKey);

        await writeRepository.HardDeleteAsync(id, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<bool>.Success(true, "Value deleted successfully");
    }
}
