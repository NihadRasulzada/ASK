using App.BL.DTOs;
using App.BL.Mapper.InternationalSolidarity;
using App.BL.Services.External;
using App.Core.Interfaces.Repository.InternationalSolidarity;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.InternationalSolidarity;

public class InternationalSolidarityService(
    IInternationalSolidarityReadRepository readRepository,
    IInternationalSolidarityWriteRepository writeRepository,
    ICloudinaryService cloudinaryService,
    IInternationalSolidarityMapper mapper) : IInternationalSolidarityService
{
    public async Task<Response<IEnumerable<InternationalSolidarityResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<Core.Entities.InternationalSolidarity> entities = await readRepository.GetAllAsync(false, cancellationToken);

        IEnumerable<InternationalSolidarityResponseDto> result = entities.Select(mapper.DomainToResponseDto);

        return Response<IEnumerable<InternationalSolidarityResponseDto>>
            .Success(result, $"{result.Count()} international solidarity records retrieved successfully");
    }

    public async Task<Response<InternationalSolidarityResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.InternationalSolidarity? entity = await readRepository.GetByIdAsync(id, false, cancellationToken);

        if (entity == null)
            return Response<InternationalSolidarityResponseDto?>.NotFound("International solidarity not found");

        return Response<InternationalSolidarityResponseDto?>.Success(mapper.DomainToResponseDto(entity), "International solidarity retrieved successfully");
    }

    public async Task<Response<InternationalSolidarityResponseDto>> CreateAsync(CreateInternationalSolidarityDto dto, CancellationToken cancellationToken = default)
    {
        string iconUrl = await cloudinaryService.UploadImageAsync(dto.Icon);

        Core.Entities.InternationalSolidarity entity = mapper.CreateDtoToDomain(dto, iconUrl);

        await writeRepository.AddAsync(entity, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<InternationalSolidarityResponseDto>.Success(mapper.DomainToResponseDto(entity), "International solidarity created successfully");
    }

    public async Task<Response<InternationalSolidarityResponseDto?>> UpdateAsync(Guid id, UpdateInternationalSolidarityDto dto, CancellationToken cancellationToken = default)
    {
        Core.Entities.InternationalSolidarity? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
            return Response<InternationalSolidarityResponseDto?>.NotFound("International solidarity not found");

        string iconUrl = entity.IconUrl;
        if (dto.Icon != null)
            iconUrl = await cloudinaryService.UploadImageAsync(dto.Icon);

        mapper.UpdateDtoToDomain(entity, dto, iconUrl);

        writeRepository.Update(entity);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<InternationalSolidarityResponseDto?>.Success(mapper.DomainToResponseDto(entity), "International solidarity updated successfully");
    }

    public async Task<Response<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.InternationalSolidarity? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
            return Response<bool>.NotFound("International solidarity not found");

        await writeRepository.HardDeleteAsync(id, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<bool>.Success(true, "International solidarity deleted successfully");
    }
}
