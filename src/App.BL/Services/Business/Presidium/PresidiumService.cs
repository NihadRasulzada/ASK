using App.BL.DTOs;
using App.BL.Mapper.Presidium;
using App.BL.Services.External;
using App.Core.Entities.Common.Cloudinary;
using App.Core.Interfaces.Repository.Presidium;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.Presidium;

public class PresidiumService(
    IPresidiumReadRepository readRepository,
    IPresidiumWriteRepository writeRepository,
    ICloudinaryService cloudinaryService,
    IPresidiumMapper mapper) : CloudinaryEntityService(cloudinaryService), IPresidiumService
{
    public async Task<Response<IEnumerable<PresidiumResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<Core.Entities.Presidium> entities = await readRepository.GetAllAsync(false, cancellationToken);

        IEnumerable<PresidiumResponseDto> result = entities.Select(mapper.DomainToResponseDto);

        return Response<IEnumerable<PresidiumResponseDto>>
            .Success(result, $"{result.Count()} presidium members retrieved successfully");
    }

    public async Task<Response<PresidiumResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.Presidium? entity = await readRepository.GetByIdAsync(id, false, cancellationToken);

        if (entity == null)
            return Response<PresidiumResponseDto?>.NotFound("Presidium member not found");

        return Response<PresidiumResponseDto?>.Success(mapper.DomainToResponseDto(entity), "Presidium member retrieved successfully");
    }

    public async Task<Response<PresidiumResponseDto>> CreateAsync(CreatePresidiumDto dto, CancellationToken cancellationToken = default)
    {
        CloudinaryURL imageUrl = await cloudinaryService.UploadImageAsync(dto.Image);

        Core.Entities.Presidium entity = mapper.CreateDtoToDomain(dto, imageUrl);

        await writeRepository.AddAsync(entity, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<PresidiumResponseDto>.Success(mapper.DomainToResponseDto(entity), "Presidium member created successfully");
    }

    public async Task<Response<PresidiumResponseDto?>> UpdateAsync(Guid id, UpdatePresidiumDto dto, CancellationToken cancellationToken = default)
    {
        Core.Entities.Presidium? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
            return Response<PresidiumResponseDto?>.NotFound("Presidium member not found");

        CloudinaryURL imageUrl = entity.ImageUrl;
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

        return Response<PresidiumResponseDto?>.Success(mapper.DomainToResponseDto(entity), "Presidium member updated successfully");
    }

    public async Task<Response<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.Presidium? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
            return Response<bool>.NotFound("Presidium member not found");

        await DeleteImageAsync(entity.ImageUrl.PublicId);

        await writeRepository.HardDeleteAsync(id, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<bool>.Success(true, "Presidium member deleted successfully");
    }
}
