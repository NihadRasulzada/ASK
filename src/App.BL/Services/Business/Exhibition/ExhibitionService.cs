using App.BL.DTOs;
using App.BL.Mapper.Exhibition;
using App.BL.Services.External;
using App.Core.Interfaces.Repository.Exhibition;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.Exhibition;

public class ExhibitionService(
    IExhibitionReadRepository readRepository,
    IExhibitionWriteRepository writeRepository,
    ICloudinaryService cloudinaryService,
    IExhibitionMapper mapper) : IExhibitionService
{
    public async Task<Response> ActivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.Exhibition? entity = await readRepository.GetByIdIncludingDeletedAsync(id, true, cancellationToken);
        if (entity == null) return Response.NotFound("Exhibition not found");
        if (!entity.IsDeactive) return Response.BadRequest("Exhibition is already active");

        entity.Activate();
        await writeRepository.SaveChangesAsync(cancellationToken);
        return Response.Success("Exhibition activated successfully");
    }

    public async Task<Response<ExhibitionResponseDto>> CreateAsync(CreateExhibitionDto dto, CancellationToken cancellationToken = default)
    {
        string imageUrl = await cloudinaryService.UploadImageAsync(dto.Image);
        Core.Entities.Exhibition entity = mapper.CreateDtoToDomain(dto, imageUrl);

        await writeRepository.AddAsync(entity, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        ExhibitionResponseDto response = mapper.DomainToResponseDto(entity);
        return Response<ExhibitionResponseDto>.Success(response, "Exhibition created successfully");
    }

    public async Task<Response> DeActivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.Exhibition? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);
        if (entity == null) return Response.NotFound("Exhibition not found");
        if (entity.IsDeactive) return Response.BadRequest("Exhibition is already deactive");

        entity.Deactivate();
        await writeRepository.SaveChangesAsync(cancellationToken);
        return Response.Success("Exhibition deactivated successfully");
    }

    public async Task<Response> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.Exhibition? entity = await readRepository.GetByIdIncludingDeletedAsync(id, true, cancellationToken);
        if (entity == null) return Response.NotFound("Exhibition not found");

        await writeRepository.HardDeleteAsync(id, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);
        return Response.Success("Exhibition deleted successfully");
    }

    public async Task<Response<IEnumerable<ExhibitionResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<Core.Entities.Exhibition> entities = await readRepository.GetAllAsync(false, cancellationToken);
        if (!entities.Any()) return Response<IEnumerable<ExhibitionResponseDto>>.Success(Enumerable.Empty<ExhibitionResponseDto>(), "No exhibitions found");

        IEnumerable<ExhibitionResponseDto> result = entities.Select(x => mapper.DomainToResponseDto(x));
        return Response<IEnumerable<ExhibitionResponseDto>>.Success(result, "Exhibitions retrieved successfully");
    }

    public async Task<Response<IEnumerable<ExhibitionResponseDto>>> GetAllIncludingDeletedAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<Core.Entities.Exhibition> entities = await readRepository.GetAllIncludingDeletedAsync(cancellationToken);
        if (!entities.Any()) return Response<IEnumerable<ExhibitionResponseDto>>.Success(Enumerable.Empty<ExhibitionResponseDto>(), "No exhibitions found");

        IEnumerable<ExhibitionResponseDto> result = entities.Select(x => mapper.DomainToResponseDto(x));
        return Response<IEnumerable<ExhibitionResponseDto>>.Success(result, "All exhibitions retrieved successfully");
    }

    public async Task<Response<ExhibitionResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.Exhibition? entity = await readRepository.GetByIdIncludingDeletedAsync(id, false, cancellationToken);
        if (entity == null) return Response<ExhibitionResponseDto?>.NotFound("Exhibition not found");

        ExhibitionResponseDto dto = mapper.DomainToResponseDto(entity);
        return Response<ExhibitionResponseDto?>.Success(dto, "Exhibition retrieved successfully");
    }

    public async Task<Response<ExhibitionResponseDto?>> UpdateAsync(Guid id, UpdateExhibitionDto dto, CancellationToken cancellationToken = default)
    {
        Core.Entities.Exhibition? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);
        if (entity == null) return Response<ExhibitionResponseDto?>.NotFound("Exhibition not found");

        string? newImageUrl = null;
        if (dto.Image != null)
        {
            newImageUrl = await cloudinaryService.UploadImageAsync(dto.Image);
        }

        mapper.UpdateDtoToDomain(entity, dto, newImageUrl);
        writeRepository.Update(entity);
        await writeRepository.SaveChangesAsync(cancellationToken);

        ExhibitionResponseDto response = mapper.DomainToResponseDto(entity);
        return Response<ExhibitionResponseDto?>.Success(response, "Exhibition updated successfully");
    }
}
