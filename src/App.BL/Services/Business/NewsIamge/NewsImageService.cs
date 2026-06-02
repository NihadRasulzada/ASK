using App.BL.DTOs;
using App.BL.Mapper.NewsImage;
using App.BL.NewsImages.Business.NewsIamge;
using App.BL.Services.External;
using App.Core.Entities.Common.Storage;
using App.Core.Interfaces.Repository.NewsImage;
using App.Core.ResponseObject.Concreate;
using Microsoft.EntityFrameworkCore;

namespace App.BL.Services.Business.NewsIamge;

public class NewsImageService : StorageEntityService , INewsImageService
{
    private readonly IStorageService storageService;
    private readonly INewsImageWriteRepository writeRepository;
    private readonly INewsImageReadRepository readRepository;
    private readonly INewsImageMapper mapper;

    public NewsImageService(IStorageService storageService, INewsImageWriteRepository writeRepository, INewsImageReadRepository readRepository, INewsImageMapper mapper): base(storageService)
    {
        this.storageService = storageService;
        this.writeRepository = writeRepository;
        this.readRepository = readRepository;
        this.mapper = mapper;
    }

    public async Task<Response> CreateAsync(CreateNewsImageDto dto, CancellationToken cancellationToken = default)
    {
        StoredFile imageUrl = await storageService.UploadAsync(dto.Image);

        Core.Entities.NewsImage entity = mapper.CreateDtoToDomain(dto, imageUrl);

        await writeRepository.AddAsync(entity, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response.Success("News image created successfully");
    }

    public async Task<Response> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        //cloudinarydana silimmelidir
        Core.Entities.NewsImage? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
            return Response.NotFound("News image not found");

        await DeleteFileAsync(entity.ImageUrl.ObjectKey);

        await writeRepository.HardDeleteAsync(id, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response.Success("News image deleted successfully");
    }

    public async Task<Response<IEnumerable<NewsImageResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<Core.Entities.NewsImage> entities = await readRepository.GetAllAsync(false, cancellationToken, false, include: x => x.Include(i => i.News));

        if (!entities.Any())
            return Response<IEnumerable<NewsImageResponseDto>>
                .Success(Enumerable.Empty<NewsImageResponseDto>(), "No news images found");

        IEnumerable<NewsImageResponseDto> result = entities.Select(x => mapper.DomainToResponseDto(x));

        return Response<IEnumerable<NewsImageResponseDto>>
            .Success(result, $"{result.Count()} news images retrieved successfully");
    }

    public async Task<Response<NewsImageResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.NewsImage? entity = await readRepository.GetByIdAsync(id, false, cancellationToken);

        if (entity == null)
            return Response<NewsImageResponseDto?>.NotFound("News image not found");

        NewsImageResponseDto dto = mapper.DomainToResponseDto(entity);

        return Response<NewsImageResponseDto?>.Success(dto, "News image retrieved successfully");
    }

    public async Task<Response<NewsImageResponseDto?>> UpdateAsync(Guid id,UpdateNewsImageDto dto, CancellationToken cancellationToken = default)
    {
        Core.Entities.NewsImage? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
            return Response<NewsImageResponseDto?>.NotFound("News image not found");

        StoredFile imageUrl = entity.ImageUrl;

        if (dto.Image != null)
        {
            var (newUrl, oldPublicId) = await ReplaceFileAsync(  
                entity.ImageUrl.ObjectKey,
                dto.Image);

            mapper.UpdateDtoToDomain(entity, dto, newUrl);
            await DeleteFileAsync(oldPublicId); 
        }
        else
        {
            mapper.UpdateDtoToDomain(entity, dto, null);
        }

        writeRepository.Update(entity);
        await writeRepository.SaveChangesAsync(cancellationToken);

        NewsImageResponseDto response = mapper.DomainToResponseDto(entity);

        return Response<NewsImageResponseDto?>.Success(response, "News image updated successfully");
    }
}