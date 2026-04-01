using App.BL.DTOs;
using App.BL.Mapper.Management;
using App.Core.Interfaces.Repository.Management;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.Management;

public class ManagementService(
    IManagementReadRepository readRepository,
    IManagementWriteRepository writeRepository,
    IManagementMapper mapper) : IManagementService
{
    public async Task<Response<IEnumerable<ManagementResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<Core.Entities.Management> entities = await readRepository.GetAllAsync(false, cancellationToken);

        IEnumerable<ManagementResponseDto> result = entities.Select(mapper.DomainToResponseDto);

        return Response<IEnumerable<ManagementResponseDto>>
            .Success(result, $"{result.Count()} management records retrieved successfully");
    }

    public async Task<Response<ManagementResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.Management? entity = await readRepository.GetByIdAsync(id, false, cancellationToken);

        if (entity == null)
            return Response<ManagementResponseDto?>.NotFound("Management not found");

        return Response<ManagementResponseDto?>.Success(mapper.DomainToResponseDto(entity), "Management retrieved successfully");
    }

    public async Task<Response<ManagementResponseDto>> CreateAsync(CreateManagementDto dto, CancellationToken cancellationToken = default)
    {
        Core.Entities.Management entity = mapper.CreateDtoToDomain(dto);

        await writeRepository.AddAsync(entity, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<ManagementResponseDto>.Success(mapper.DomainToResponseDto(entity), "Management created successfully");
    }

    public async Task<Response<ManagementResponseDto?>> UpdateAsync(Guid id, UpdateManagementDto dto, CancellationToken cancellationToken = default)
    {
        Core.Entities.Management? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
            return Response<ManagementResponseDto?>.NotFound("Management not found");

        mapper.UpdateDtoToDomain(entity, dto);

        writeRepository.Update(entity);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<ManagementResponseDto?>.Success(mapper.DomainToResponseDto(entity), "Management updated successfully");
    }

    public async Task<Response<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.Management? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
            return Response<bool>.NotFound("Management not found");

        await writeRepository.HardDeleteAsync(id, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<bool>.Success(true, "Management deleted successfully");
    }
}
