using App.BL.DTOs;
using App.BL.Mapper.Exhibition;
using App.BL.Services.External;
using App.Core.Entities.Common.Cloudinary;
using App.Core.Interfaces.Repository.Exhibition;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.Exhibition;

public class ExhibitionService(
    IExhibitionReadRepository readRepository,
    IExhibitionWriteRepository writeRepository,
    ICloudinaryService cloudinaryService,
    IExhibitionMapper mapper) : CloudinaryEntityService(cloudinaryService), IExhibitionService
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
        CloudinaryURL imageUrl = await cloudinaryService.UploadImageAsync(dto.Image);
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

        await DeleteImageAsync(entity.TitleImageUrl.PublicId);

        await writeRepository.HardDeleteAsync(id, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);
        return Response.Success("Exhibition deleted successfully");
    }

    public async Task<PagedResponse<IEnumerable<ExhibitionResponseDto>>> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
    {
        var (items, totalCount) = await readRepository.GetPagedAsync(false, false, pageIndex, pageSize, cancellationToken);
        var result = items.Select(mapper.DomainToResponseDto);
        return PagedResponse<IEnumerable<ExhibitionResponseDto>>.Create(result, pageIndex, pageSize, totalCount, "Exhibitions retrieved successfully");
    }

    public async Task<PagedResponse<IEnumerable<ExhibitionDateResponseDto>>> GetAllDateAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
    {
        var (items, totalCount) = await readRepository.GetPagedAsync(false, false, pageIndex, pageSize, cancellationToken);
        var result = items.Select(mapper.DomainToResponseDateDto);
        return PagedResponse<IEnumerable<ExhibitionDateResponseDto>>.Create(result, pageIndex, pageSize, totalCount, "Exhibitions retrieved successfully");
    }

    public async Task<PagedResponse<IEnumerable<ExhibitionResponseDto>>> GetAllIncludingDeletedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
    {
        var (items, totalCount) = await readRepository.GetPagedAsync(false, true, pageIndex, pageSize, cancellationToken);
        var result = items.Select(mapper.DomainToResponseDto);
        return PagedResponse<IEnumerable<ExhibitionResponseDto>>.Create(result, pageIndex, pageSize, totalCount, "All exhibitions retrieved successfully");
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

        CloudinaryURL? newImageUrl = null;
        if (dto.Image != null)
        {
            var (newUrl, oldPublicId) = await ReplaceImageAsync(  
                entity.TitleImageUrl.PublicId,
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

        ExhibitionResponseDto response = mapper.DomainToResponseDto(entity);
        return Response<ExhibitionResponseDto?>.Success(response, "Exhibition updated successfully");
    }
}
