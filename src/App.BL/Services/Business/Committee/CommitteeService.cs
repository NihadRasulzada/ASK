using App.BL.DTOs;
using App.BL.Mapper.Committee;
using App.Core.Interfaces.Repository.Committee;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.Committee;

public class CommitteeService(
    ICommitteeReadRepository readRepository,
    ICommitteeWriteRepository writeRepository,
    ICommitteeMapper mapper) : ICommitteeService
{
    public async Task<Response<IEnumerable<CommitteeResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<Core.Entities.Committee> entities = await readRepository.GetAllAsync(false, cancellationToken);

        IEnumerable<CommitteeResponseDto> result = entities.Select(mapper.DomainToResponseDto);

        return Response<IEnumerable<CommitteeResponseDto>>
            .Success(result, $"{result.Count()} committees retrieved successfully");
    }

    public async Task<Response<CommitteeResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.Committee? entity = await readRepository.GetByIdAsync(id, false, cancellationToken);

        if (entity == null)
            return Response<CommitteeResponseDto?>.NotFound("Committee not found");

        return Response<CommitteeResponseDto?>.Success(mapper.DomainToResponseDto(entity), "Committee retrieved successfully");
    }

    public async Task<Response<CommitteeResponseDto>> CreateAsync(CreateCommitteeDto dto, CancellationToken cancellationToken = default)
    {
        Core.Entities.Committee entity = mapper.CreateDtoToDomain(dto);

        await writeRepository.AddAsync(entity, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<CommitteeResponseDto>.Success(mapper.DomainToResponseDto(entity), "Committee created successfully");
    }

    public async Task<Response<CommitteeResponseDto?>> UpdateAsync(Guid id, UpdateCommitteeDto dto, CancellationToken cancellationToken = default)
    {
        Core.Entities.Committee? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
            return Response<CommitteeResponseDto?>.NotFound("Committee not found");

        mapper.UpdateDtoToDomain(entity, dto);

        writeRepository.Update(entity);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<CommitteeResponseDto?>.Success(mapper.DomainToResponseDto(entity), "Committee updated successfully");
    }

    public async Task<Response<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.Committee? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
            return Response<bool>.NotFound("Committee not found");

        await writeRepository.HardDeleteAsync(id, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<bool>.Success(true, "Committee deleted successfully");
    }
}
