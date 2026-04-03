using App.BL.DTOs;
using App.BL.Mapper.BusinessForum;
using App.BL.Services.External;
using App.Core.Interfaces.Repository.BusinessForum;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.BusinessForum;

public class BusinessForumService(
    IBusinessForumReadRepository readRepository,
    IBusinessForumWriteRepository writeRepository,
    ICloudinaryService cloudinaryService,
    IBusinessForumMapper mapper) : IBusinessForumService
{
    public async Task<PagedResponse<IEnumerable<BusinessForumResponseDto>>> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
    {
        var (items, totalCount) = await readRepository.GetPagedAsync(false, false, pageIndex, pageSize, cancellationToken);
        var result = items.Select(mapper.DomainToResponseDto);
        return PagedResponse<IEnumerable<BusinessForumResponseDto>>.Create(result, pageIndex, pageSize, totalCount, "Business forums retrieved successfully");
    }

    public async Task<Response<BusinessForumResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await readRepository.GetByIdAsync(id, false, cancellationToken);
        if (entity is null) return Response<BusinessForumResponseDto?>.NotFound("Business forum not found");

        return Response<BusinessForumResponseDto?>.Success(mapper.DomainToResponseDto(entity), "Business forum retrieved successfully");
    }

    public async Task<Response<BusinessForumResponseDto>> CreateAsync(CreateBusinessForumDto dto, CancellationToken cancellationToken = default)
    {
        string titleImageUrl = await cloudinaryService.UploadImageAsync(dto.TitleImage);
        string detailImageUrl = await cloudinaryService.UploadImageAsync(dto.DetailImage);

        var entity = mapper.CreateDtoToDomain(dto, titleImageUrl, detailImageUrl);
        await writeRepository.AddAsync(entity, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<BusinessForumResponseDto>.Success(mapper.DomainToResponseDto(entity), "Business forum created successfully");
    }

    public async Task<Response<BusinessForumResponseDto?>> UpdateAsync(Guid id, UpdateBusinessForumDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await readRepository.GetByIdAsync(id, true, cancellationToken);
        if (entity is null) return Response<BusinessForumResponseDto?>.NotFound("Business forum not found");

        string? newTitleImageUrl = null;
        if (dto.TitleImage is not null)
            newTitleImageUrl = await cloudinaryService.UploadImageAsync(dto.TitleImage);

        string? newDetailImageUrl = null;
        if (dto.DetailImage is not null)
            newDetailImageUrl = await cloudinaryService.UploadImageAsync(dto.DetailImage);

        mapper.UpdateDtoToDomain(entity, dto, newTitleImageUrl, newDetailImageUrl);
        writeRepository.Update(entity);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<BusinessForumResponseDto?>.Success(mapper.DomainToResponseDto(entity), "Business forum updated successfully");
    }

    public async Task<Response> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await readRepository.GetByIdAsync(id, true, cancellationToken);
        if (entity is null) return Response.NotFound("Business forum not found");

        await writeRepository.HardDeleteAsync(id, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);
        return Response.Success("Business forum deleted successfully");
    }
}
