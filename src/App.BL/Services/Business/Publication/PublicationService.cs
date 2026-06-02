using App.BL.DTOs;
using App.BL.Mapper.Publication;
using App.BL.Services.External;
using App.Core.Entities.Common.Storage;
using App.Core.Interfaces.Repository.Publication;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.Publication;

public class PublicationService(
    IPublicationReadRepository readRepository,
    IPublicationWriteRepository writeRepository,
    IStorageService storageService,
    IPublicationMapper mapper) : StorageEntityService(storageService), IPublicationService
{
    public async Task<PagedResponse<IEnumerable<PublicationResponseDto>>> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
    {
        var (items, totalCount) = await readRepository.GetPagedAsync(false, false, pageIndex, pageSize, cancellationToken);
        var result = items.Select(mapper.DomainToResponseDto);
        return PagedResponse<IEnumerable<PublicationResponseDto>>.Create(result, pageIndex, pageSize, totalCount, "Publications retrieved successfully");
    }

    public async Task<Response<PublicationResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await readRepository.GetByIdAsync(id, false, cancellationToken);
        if (entity is null) return Response<PublicationResponseDto?>.NotFound("Publication not found");

        return Response<PublicationResponseDto?>.Success(mapper.DomainToResponseDto(entity), "Publication retrieved successfully");
    }

    public async Task<Response<PublicationResponseDto>> CreateAsync(CreatePublicationDto dto, CancellationToken cancellationToken = default)
    {
        StoredFile titleImageUrl = await storageService.UploadAsync(dto.TitleImage);
        StoredFile pdfUrl = await storageService.UploadAsync(dto.PdfFile);

        var entity = mapper.CreateDtoToDomain(dto, titleImageUrl, pdfUrl);
        await writeRepository.AddAsync(entity, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<PublicationResponseDto>.Success(mapper.DomainToResponseDto(entity), "Publication created successfully");
    }

    public async Task<Response<PublicationResponseDto?>> UpdateAsync(Guid id, UpdatePublicationDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await readRepository.GetByIdAsync(id, true, cancellationToken);
        if (entity is null) return Response<PublicationResponseDto?>.NotFound("Publication not found");

        StoredFile? newTitleImageUrl = null;
        if (dto.TitleImage != null)
        {
            var (newUrl, oldPublicId) = await ReplaceFileAsync(
                entity.TitleImageUrl.ObjectKey,
                dto.TitleImage);

            mapper.UpdateDtoToDomain(entity, dto, newUrl);
            await DeleteFileAsync(oldPublicId);
        }
        else
        {
            mapper.UpdateDtoToDomain(entity, dto, null);
        }

        StoredFile? newPdfUrl = null;
        if (dto.PdfFile != null)
        {
            var (newUrl, oldPublicId) = await ReplaceFileAsync(
                entity.PdfUrl.ObjectKey,
                dto.PdfFile);

            mapper.UpdateDtoToDomain(entity, dto, newUrl);
            await DeleteFileAsync(oldPublicId);
        }
        else
        {
            mapper.UpdateDtoToDomain(entity, dto, null);
        }

        writeRepository.Update(entity);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<PublicationResponseDto?>.Success(mapper.DomainToResponseDto(entity), "Publication updated successfully");
    }

    public async Task<Response> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await readRepository.GetByIdAsync(id, true, cancellationToken);
        if (entity is null) return Response.NotFound("Publication not found");

        await DeleteFileAsync(entity.TitleImageUrl.ObjectKey);   //imahge silmey ucun 
        await DeleteFileAsync(entity.PdfUrl.ObjectKey);          // pdf silmey ucun

        await writeRepository.HardDeleteAsync(id, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);
        return Response.Success("Publication deleted successfully");
    }
}
