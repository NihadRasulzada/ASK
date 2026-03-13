using App.BL.DTOs;
using App.BL.Mapper.News;
using App.BL.Services.External;
using App.Core.Interfaces.Repository.News;
using App.Core.ResponseObject.Concreate;
using App.DAL.Context;

namespace App.BL.Services.Business.News;
//TODO: Full struktur 0 dan yazilacag
public class NewsService(
    INewsReadRepository readRepository,
    INewsWriteRepository writeRepository,
    ICloudinaryService cloudinaryService,
    INewsMapper newsMapper) : INewsService
{

    public async Task<Response> ActivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.News? entity = await readRepository.GetByIdIncludingDeletedAsync(id, true, cancellationToken);

        if (entity == null)
            return Response.NotFound("News not found");

        if (!entity.IsDeactive)
            return Response.BadRequest("News is already active");

        entity.Activate();

        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response.Success("News activated successfully");
    }

    public async Task<Response<NewsResponseDto>> CreateAsync(CreateNewsDto dto, CancellationToken cancellationToken = default)
    {
        string titleImageUrl = await cloudinaryService.UploadImageAsync(dto.TitleImage);

        List<string> imageUrls = new();

        if (dto.AdditionalImages != null && dto.AdditionalImages.Any())
        {
            foreach (var image in dto.AdditionalImages)
            {
                string url = await cloudinaryService.UploadImageAsync(image);
                imageUrls.Add(url);
            }
        }

        Core.Entities.News entity = newsMapper.CreateDtoToDomain(dto, titleImageUrl, imageUrls);

        await writeRepository.AddAsync(entity, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        NewsResponseDto response = newsMapper.DomainToResponseDto(entity);

        return Response<NewsResponseDto>.Success(response, "News created successfully");
    }

    public async Task<Response> DeActivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.News? entity = await readRepository.GetByIdIncludingDeletedAsync(id, true, cancellationToken);

        if (entity == null)
            return Response.NotFound("News not found");

        if (entity.IsDeactive)
            return Response.BadRequest("News is already deactive");

        entity.Deactivate();

        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response.Success("News deactivated successfully");
    }

    public async Task<Response<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.News? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
            return Response<bool>.NotFound("News not found");

        await writeRepository.HardDeleteAsync(id, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<bool>.Success(true, "News deleted successfully");
    }

    public async Task<Response<IEnumerable<NewsResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<Core.Entities.News> entities = await readRepository.GetAllAsync(false, cancellationToken, false);

        if (!entities.Any())
            return Response<IEnumerable<NewsResponseDto>>
                .Success(Enumerable.Empty<NewsResponseDto>(), "No news found");

        IEnumerable<NewsResponseDto> result =
            entities.Select(x => newsMapper.DomainToResponseDto(x));

        return Response<IEnumerable<NewsResponseDto>>
            .Success(result, $"{result.Count()} news retrieved successfully");
    }

    public async Task<Response<IEnumerable<NewsResponseDto>>> GetAllIncludingDeletedAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<Core.Entities.News> entities = await readRepository.GetAllIncludingDeletedAsync(cancellationToken);

        if (!entities.Any())
            return Response<IEnumerable<NewsResponseDto>>
                .Success(Enumerable.Empty<NewsResponseDto>(), "No news found");

        IEnumerable<NewsResponseDto> result =
            entities.Select(x => newsMapper.DomainToResponseDto(x));

        return Response<IEnumerable<NewsResponseDto>>
            .Success(result, $"{result.Count()} news retrieved successfully");
    }

    public async Task<Response<NewsResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.News? entity = await readRepository.GetByIdAsync(id, false, cancellationToken);

        if (entity == null)
            return Response<NewsResponseDto?>.NotFound("News not found");

        NewsResponseDto dto = newsMapper.DomainToResponseDto(entity);

        return Response<NewsResponseDto?>.Success(dto, "News retrieved successfully");
    }

    public async Task<Response<NewsResponseDto?>> UpdateAsync(Guid id, UpdateNewsDto dto, CancellationToken cancellationToken = default)
    {
        Core.Entities.News? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
            return Response<NewsResponseDto?>.NotFound("News not found");

        string titleImageUrl = entity.TitleImageUrl;

        if (dto.TitleImage != null)
        {
            titleImageUrl = await cloudinaryService.UploadImageAsync(dto.TitleImage);
        }

        List<string> imageUrls = entity.ImageUrls?.ToList() ?? new List<string>();

        if (dto.AdditionalImages != null && dto.AdditionalImages.Any())
        {
            imageUrls.Clear();

            foreach (var image in dto.AdditionalImages)
            {
                string url = await cloudinaryService.UploadImageAsync(image);
                imageUrls.Add(url);
            }
        }

        newsMapper.UpdateDtoToDomain(entity, dto, titleImageUrl, imageUrls);

        writeRepository.Update(entity);
        await writeRepository.SaveChangesAsync(cancellationToken);

        NewsResponseDto response = newsMapper.DomainToResponseDto(entity);

        return Response<NewsResponseDto?>.Success(response, "News updated successfully");
    }
}