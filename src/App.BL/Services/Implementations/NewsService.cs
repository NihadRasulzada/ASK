using App.BL.DTOs;
using App.BL.Services.Abstractions;
using App.Core.Entities;
using App.Core.Interfaces;

namespace App.BL.Services.Implementations;

public class NewsService : INewsService
{
    private readonly INewsRepository _newsRepository;
    private readonly ICloudinaryService _cloudinaryService;

    public NewsService(INewsRepository newsRepository, ICloudinaryService cloudinaryService)
    {
        _newsRepository = newsRepository;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<IEnumerable<NewsResponseDto>> GetAllAsync()
    {
        var newsList = await _newsRepository.GetAllAsync();
        return newsList.Select(MapToResponseDto);
    }

    public async Task<NewsResponseDto?> GetByIdAsync(Guid id)
    {
        var news = await _newsRepository.GetByIdAsync(id);
        return news is null ? null : MapToResponseDto(news);
    }

    public async Task<NewsResponseDto> CreateAsync(CreateNewsDto dto)
    {
        // Başlıq şəklini Cloudinary-yə yüklə
        var titleImageUrl = await _cloudinaryService.UploadImageAsync(dto.TitleImage);

        // Əlavə şəkilləri yüklə (varsa)
        IList<string> imageUrls = dto.AdditionalImages is { Count: > 0 }
            ? await _cloudinaryService.UploadImagesAsync(dto.AdditionalImages)
            : new List<string>();

        var news = new News(titleImageUrl, dto.NewsText, imageUrls);
        await _newsRepository.AddAsync(news);
        await _newsRepository.SaveChangesAsync();

        return MapToResponseDto(news);
    }

    public async Task<NewsResponseDto?> UpdateAsync(Guid id, UpdateNewsDto dto)
    {
        var news = await _newsRepository.GetByIdAsync(id);
        if (news is null)
            return null;

        // Yeni başlıq şəkli göndərilibsə — yüklə, yoxsa null (entity köhnəni saxlayır)
        string? titleImageUrl = null;
        if (dto.TitleImage is not null)
            titleImageUrl = await _cloudinaryService.UploadImageAsync(dto.TitleImage);

        // Yeni əlavə şəkillər göndərilibsə — yüklə, yoxsa null (entity köhnəni saxlayır)
        IList<string>? imageUrls = null;
        if (dto.AdditionalImages is { Count: > 0 })
            imageUrls = await _cloudinaryService.UploadImagesAsync(dto.AdditionalImages);

        news.Update(titleImageUrl, dto.NewsText, imageUrls);
        _newsRepository.Update(news);
        await _newsRepository.SaveChangesAsync();

        return MapToResponseDto(news);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var news = await _newsRepository.GetByIdAsync(id);
        if (news is null)
            return false;

        await _newsRepository.DeleteAsync(id);
        await _newsRepository.SaveChangesAsync();
        return true;
    }

    /// <inheritdoc/>
    public Task<bool> ActivateAsync(Guid id)
        => _newsRepository.SetActiveStatusAsync(id, true);

    /// <inheritdoc/>
    public Task<bool> DeactivateAsync(Guid id)
        => _newsRepository.SetActiveStatusAsync(id, false);

    private static NewsResponseDto MapToResponseDto(News news)
        => new(news.Id, news.TitleImageUrl, news.NewsText, news.ImageUrls, news.CreatedOn, news.IsActive);
}
