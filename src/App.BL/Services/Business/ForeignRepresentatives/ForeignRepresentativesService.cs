using App.BL.DTOs;
using App.BL.Mapper.ForeignRepresentatives;
using App.Core.Interfaces.Repository.ForeignRepresentatives;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.ForeignRepresentatives;

public class ForeignRepresentativesService(
    IForeignRepresentativesReadRepository readRepository,
    IForeignRepresentativesWriteRepository writeRepository,
    IForeignRepresentativesMapper mapper) : IForeignRepresentativesService
{
    public async Task<Response<IEnumerable<ForeignRepresentativesResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<Core.Entities.ForeignRepresentatives> entities = await readRepository.GetAllAsync(false, cancellationToken);

        IEnumerable<ForeignRepresentativesResponseDto> result = entities.Select(mapper.DomainToResponseDto);

        return Response<IEnumerable<ForeignRepresentativesResponseDto>>
            .Success(result, $"{result.Count()} foreign representatives retrieved successfully");
    }

    public async Task<Response<ForeignRepresentativesResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.ForeignRepresentatives? entity = await readRepository.GetByIdAsync(id, false, cancellationToken);

        if (entity == null)
            return Response<ForeignRepresentativesResponseDto?>.NotFound("Foreign representative not found");

        return Response<ForeignRepresentativesResponseDto?>.Success(mapper.DomainToResponseDto(entity), "Foreign representative retrieved successfully");
    }

    public async Task<Response<ForeignRepresentativesResponseDto>> CreateAsync(CreateForeignRepresentativesDto dto, CancellationToken cancellationToken = default)
    {
        Core.Entities.ForeignRepresentatives entity = mapper.CreateDtoToDomain(dto);

        await writeRepository.AddAsync(entity, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<ForeignRepresentativesResponseDto>.Success(mapper.DomainToResponseDto(entity), "Foreign representative created successfully");
    }

    public async Task<Response<ForeignRepresentativesResponseDto?>> UpdateAsync(Guid id, UpdateForeignRepresentativesDto dto, CancellationToken cancellationToken = default)
    {
        Core.Entities.ForeignRepresentatives? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
            return Response<ForeignRepresentativesResponseDto?>.NotFound("Foreign representative not found");

        mapper.UpdateDtoToDomain(entity, dto);

        writeRepository.Update(entity);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<ForeignRepresentativesResponseDto?>.Success(mapper.DomainToResponseDto(entity), "Foreign representative updated successfully");
    }

    public async Task<Response<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.ForeignRepresentatives? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
            return Response<bool>.NotFound("Foreign representative not found");

        await writeRepository.HardDeleteAsync(id, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<bool>.Success(true, "Foreign representative deleted successfully");
    }
}
