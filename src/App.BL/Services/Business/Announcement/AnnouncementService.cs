using App.BL.DTOs;
using App.BL.Mapper.Announcement;
using App.BL.Services.External;
using App.Core.Entities.Common.Storage;
using App.Core.Interfaces.Repository.Announcement;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.Announcement;

public class AnnouncementService(
    IAnnouncementReadRepository readRepository,
    IAnnouncementWriteRepository writeRepository,
    IStorageService storageService,
    IAnnouncementMapper mapper) : StorageEntityService(storageService), IAnnouncementService
{
    public async Task<Response> CreateAsync(CreateAnnouncementDto dto, CancellationToken cancellationToken = default)
    {
        StoredFile titleImageUrl = await storageService.UploadAsync(dto.TitleImage);

        Core.Entities.Announcement entity = mapper.CreateDtoToDomain(dto, titleImageUrl);

        await writeRepository.AddAsync(entity, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response.Success("Announcement created successfully");
    }

    public async Task<Response> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.Announcement? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
            return Response.NotFound("Announcement not found");

        await DeleteFileAsync(entity.TitleImageUrl.ObjectKey);

        await writeRepository.HardDeleteAsync(id, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response.Success("Announcement deleted successfully");
    }

    public async Task<Response<IEnumerable<AnnouncementResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<Core.Entities.Announcement> entities = await readRepository.GetAllAsync(false, cancellationToken);

        if (!entities.Any())
            return Response<IEnumerable<AnnouncementResponseDto>>
                .Success(Enumerable.Empty<AnnouncementResponseDto>(), "No announcements found");

        IEnumerable<AnnouncementResponseDto> result = entities.Select(x => mapper.DomainToResponseDto(x));

        return Response<IEnumerable<AnnouncementResponseDto>>
            .Success(result, $"{result.Count()} announcements retrieved successfully");
    }

    public async Task<Response<AnnouncementResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.Announcement? entity = await readRepository.GetByIdAsync(id, false, cancellationToken);

        if (entity == null)
            return Response<AnnouncementResponseDto?>.NotFound("Announcement not found");

        AnnouncementResponseDto dto = mapper.DomainToResponseDto(entity);

        return Response<AnnouncementResponseDto?>.Success(dto, "Announcement retrieved successfully");
    }

    public async Task<Response<AnnouncementResponseDto?>> UpdateAsync(Guid id, UpdateAnnouncementDto dto, CancellationToken cancellationToken = default)
    {
        Core.Entities.Announcement? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
            return Response<AnnouncementResponseDto?>.NotFound("Announcement not found");

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

        AnnouncementResponseDto response = mapper.DomainToResponseDto(entity);

        return Response<AnnouncementResponseDto?>.Success(response, "Announcement updated successfully");
    }
}
