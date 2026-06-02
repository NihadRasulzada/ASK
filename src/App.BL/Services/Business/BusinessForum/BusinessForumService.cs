using App.BL.DTOs;
using App.BL.Mapper.BusinessForum;
using App.BL.Services.External;
using App.Core.Entities.Common.Storage;
using App.Core.Interfaces.Repository.BusinessForum;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.BusinessForum;

public class BusinessForumService(
    IBusinessForumReadRepository readRepository,
    IBusinessForumWriteRepository writeRepository,
    IStorageService storageService,
    IBusinessForumMapper mapper) : StorageEntityService(storageService), IBusinessForumService
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
        StoredFile titleImageUrl = await storageService.UploadAsync(dto.TitleImage);
        StoredFile detailImageUrl = await storageService.UploadAsync(dto.DetailImage);

        var entity = mapper.CreateDtoToDomain(dto, titleImageUrl, detailImageUrl);
        await writeRepository.AddAsync(entity, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<BusinessForumResponseDto>.Success(mapper.DomainToResponseDto(entity), "Business forum created successfully");
    }

    public async Task<Response<BusinessForumResponseDto?>> UpdateAsync(Guid id, UpdateBusinessForumDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await readRepository.GetByIdAsync(id, true, cancellationToken);
        if (entity is null) return Response<BusinessForumResponseDto?>.NotFound("Business forum not found");

        if (dto.TitleImage != null)
        {
            var (newUrl, oldPublicId) = await ReplaceFileAsync(  // 👈 base metoddan
                entity.TitleImageUrl.ObjectKey,
                dto.TitleImage);

            mapper.UpdateDtoToDomain(entity, dto, newUrl);
            await DeleteFileAsync(oldPublicId); // upload uğurlu oldu, indi sil
        }
        else
        {
            mapper.UpdateDtoToDomain(entity, dto, null);
        }

        writeRepository.Update(entity);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<BusinessForumResponseDto?>.Success(mapper.DomainToResponseDto(entity), "Business forum updated successfully");
    }

    public async Task<Response> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await readRepository.GetByIdAsync(id, true, cancellationToken);
        if (entity is null) return Response.NotFound("Business forum not found");

        await DeleteFileAsync(entity.TitleImageUrl.ObjectKey);

        await writeRepository.HardDeleteAsync(id, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);
        return Response.Success("Business forum deleted successfully");
    }
}
