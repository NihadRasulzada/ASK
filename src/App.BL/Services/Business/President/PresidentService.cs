using App.BL.DTOs;
using App.BL.Mapper.President;
using App.BL.Services.External;
using App.Core.Entities.Common.Cloudinary;
using App.Core.Interfaces.Repository.President;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.President;

public class PresidentService(
    IPresidentReadRepository readRepository,
    IPresidentWriteRepository writeRepository,
    ICloudinaryService cloudinaryService,
    IPresidentMapper mapper) : CloudinaryEntityService(cloudinaryService), IPresidentService
{
    public async Task<Response> CreateAsync(CreatePresidentDto dto, CancellationToken cancellationToken = default)
    {
        CloudinaryURL imageUrl = await cloudinaryService.UploadImageAsync(dto.Image);

        Core.Entities.President entity = mapper.CreateDtoToDomain(dto, imageUrl);

        await writeRepository.AddAsync(entity, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response.Success("President info created successfully");
    }

    public async Task<Response> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.President? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
            return Response.NotFound("President info not found");

        await DeleteImageAsync(entity.ImageUrl.PublicId);

        await writeRepository.HardDeleteAsync(id, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response.Success("President info deleted successfully");
    }

    public async Task<Response<IEnumerable<PresidentResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<Core.Entities.President> entities = await readRepository.GetAllAsync(false, cancellationToken);

        if (!entities.Any())
            return Response<IEnumerable<PresidentResponseDto>>
                .Success(Enumerable.Empty<PresidentResponseDto>(), "No president info found");

        IEnumerable<PresidentResponseDto> result = entities.Select(x => mapper.DomainToResponseDto(x));

        return Response<IEnumerable<PresidentResponseDto>>
            .Success(result, $"President info retrieved successfully");
    }

    public async Task<Response<PresidentResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.President? entity = await readRepository.GetByIdAsync(id, false, cancellationToken);

        if (entity == null)
            return Response<PresidentResponseDto?>.NotFound("President info not found");

        PresidentResponseDto dto = mapper.DomainToResponseDto(entity);

        return Response<PresidentResponseDto?>.Success(dto, "President info retrieved successfully");
    }

    public async Task<Response<PresidentResponseDto?>> UpdateAsync(Guid id, UpdatePresidentDto dto, CancellationToken cancellationToken = default)
    {
        Core.Entities.President? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
            return Response<PresidentResponseDto?>.NotFound("President info not found");

        CloudinaryURL? newImageUrl = null;
        if (dto.Image != null)
        {
            var (newUrl, oldPublicId) = await ReplaceImageAsync(  
                entity.ImageUrl.PublicId,
                dto.Image);

            mapper.UpdateDtoToDomain(entity, dto, newUrl);
            await DeleteImageAsync(oldPublicId); 
        }
        else
        {
            mapper.UpdateDtoToDomain(entity, dto, null);
        }

        writeRepository.Update(entity);
        await writeRepository.SaveChangesAsync(cancellationToken);

        PresidentResponseDto response = mapper.DomainToResponseDto(entity);

        return Response<PresidentResponseDto?>.Success(response, "President info updated successfully");
    }
}
