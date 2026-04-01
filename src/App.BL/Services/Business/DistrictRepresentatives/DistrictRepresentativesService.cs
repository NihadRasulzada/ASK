using App.BL.DTOs;
using App.BL.Mapper.DistrictRepresentatives;
using App.Core.Interfaces.Repository.DistrictRepresentatives;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.DistrictRepresentatives;

public class DistrictRepresentativesService(
    IDistrictRepresentativesReadRepository readRepository,
    IDistrictRepresentativesWriteRepository writeRepository,
    IDistrictRepresentativesMapper mapper) : IDistrictRepresentativesService
{
    public async Task<Response<IEnumerable<DistrictRepresentativesResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<Core.Entities.DistrictRepresentatives> entities = await readRepository.GetAllAsync(false, cancellationToken);

        IEnumerable<DistrictRepresentativesResponseDto> result = entities.Select(mapper.DomainToResponseDto);

        return Response<IEnumerable<DistrictRepresentativesResponseDto>>
            .Success(result, $"{result.Count()} district representatives retrieved successfully");
    }

    public async Task<Response<DistrictRepresentativesResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.DistrictRepresentatives? entity = await readRepository.GetByIdAsync(id, false, cancellationToken);

        if (entity == null)
            return Response<DistrictRepresentativesResponseDto?>.NotFound("District representative not found");

        return Response<DistrictRepresentativesResponseDto?>.Success(mapper.DomainToResponseDto(entity), "District representative retrieved successfully");
    }

    public async Task<Response<DistrictRepresentativesResponseDto>> CreateAsync(CreateDistrictRepresentativesDto dto, CancellationToken cancellationToken = default)
    {
        Core.Entities.DistrictRepresentatives entity = mapper.CreateDtoToDomain(dto);

        await writeRepository.AddAsync(entity, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<DistrictRepresentativesResponseDto>.Success(mapper.DomainToResponseDto(entity), "District representative created successfully");
    }

    public async Task<Response<DistrictRepresentativesResponseDto?>> UpdateAsync(Guid id, UpdateDistrictRepresentativesDto dto, CancellationToken cancellationToken = default)
    {
        Core.Entities.DistrictRepresentatives? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
            return Response<DistrictRepresentativesResponseDto?>.NotFound("District representative not found");

        mapper.UpdateDtoToDomain(entity, dto);

        writeRepository.Update(entity);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<DistrictRepresentativesResponseDto?>.Success(mapper.DomainToResponseDto(entity), "District representative updated successfully");
    }

    public async Task<Response<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.DistrictRepresentatives? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
            return Response<bool>.NotFound("District representative not found");

        await writeRepository.HardDeleteAsync(id, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<bool>.Success(true, "District representative deleted successfully");
    }
}
