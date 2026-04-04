using App.BL.DTOs;
using App.BL.Mapper.UsefulLink;
using App.Core.Interfaces.Repository.UsefulLink;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.UsefulLink;

public class UsefulLinkService(
    IUsefulLinkReadRepository readRepository,
    IUsefulLinkWriteRepository writeRepository,
    IUsefulLinkMapper mapper) : IUsefulLinkService
{
    public async Task<Response<IEnumerable<UsefulLinkResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var items = await readRepository.GetAllAsync(false, cancellationToken);
        return Response<IEnumerable<UsefulLinkResponseDto>>.Success(items.Select(mapper.DomainToResponseDto), "Useful links retrieved successfully");
    }

    public async Task<Response<IEnumerable<UsefulLinkResponseDto>>> GetAllIncludingDeletedAsync(CancellationToken cancellationToken = default)
    {
        var items = await readRepository.GetAllIncludingDeletedAsync(cancellationToken);
        return Response<IEnumerable<UsefulLinkResponseDto>>.Success(items.Select(mapper.DomainToResponseDto), "All useful links retrieved successfully");
    }

    public async Task<Response<UsefulLinkResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await readRepository.GetByIdIncludingDeletedAsync(id, false, cancellationToken);
        if (entity is null) return Response<UsefulLinkResponseDto?>.NotFound("Useful link not found");

        return Response<UsefulLinkResponseDto?>.Success(mapper.DomainToResponseDto(entity), "Useful link retrieved successfully");
    }

    public async Task<Response<UsefulLinkResponseDto>> CreateAsync(CreateUsefulLinkDto dto, CancellationToken cancellationToken = default)
    {
        var entity = mapper.CreateDtoToDomain(dto);
        await writeRepository.AddAsync(entity, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<UsefulLinkResponseDto>.Success(mapper.DomainToResponseDto(entity), "Useful link created successfully");
    }

    public async Task<Response<UsefulLinkResponseDto?>> UpdateAsync(Guid id, UpdateUsefulLinkDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await readRepository.GetByIdAsync(id, true, cancellationToken);
        if (entity is null) return Response<UsefulLinkResponseDto?>.NotFound("Useful link not found");

        mapper.UpdateDtoToDomain(entity, dto);
        writeRepository.Update(entity);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<UsefulLinkResponseDto?>.Success(mapper.DomainToResponseDto(entity), "Useful link updated successfully");
    }

    public async Task<Response> ActivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await readRepository.GetByIdIncludingDeletedAsync(id, true, cancellationToken);
        if (entity is null) return Response.NotFound("Useful link not found");
        if (!entity.IsDeactive) return Response.BadRequest("Useful link is already active");

        entity.Activate();
        await writeRepository.SaveChangesAsync(cancellationToken);
        return Response.Success("Useful link activated successfully");
    }

    public async Task<Response> DeActivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await readRepository.GetByIdAsync(id, true, cancellationToken);
        if (entity is null) return Response.NotFound("Useful link not found");
        if (entity.IsDeactive) return Response.BadRequest("Useful link is already deactive");

        entity.Deactivate();
        await writeRepository.SaveChangesAsync(cancellationToken);
        return Response.Success("Useful link deactivated successfully");
    }

    public async Task<Response> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await readRepository.GetByIdIncludingDeletedAsync(id, true, cancellationToken);
        if (entity is null) return Response.NotFound("Useful link not found");

        await writeRepository.HardDeleteIncludingDeletedAsync(id, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);
        return Response.Success("Useful link deleted successfully");
    }
}
