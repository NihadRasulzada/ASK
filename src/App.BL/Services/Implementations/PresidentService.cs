using App.BL.DTOs;
using App.BL.Services.Abstractions;
using App.Core.Entities;
using App.Core.Interfaces;

namespace App.BL.Services.Implementations;

public class PresidentService : IPresidentService
{
    private readonly IPresidentRepository _repository;
    private readonly ICloudinaryService _cloudinaryService;

    public PresidentService(IPresidentRepository repository, ICloudinaryService cloudinaryService)
    {
        _repository = repository;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<PresidentResponseDto?> GetAsync()
    {
        var president = await _repository.GetSingleAsync();
        return president is null ? null : MapToDto(president);
    }

    public async Task<PresidentResponseDto?> CreateAsync(CreatePresidentDto dto)
    {
        // Singleton məhdudiyyəti: DB-də artıq prezident varsa yaratmağa icazə yoxdur
        if (await _repository.GetSingleAsync() is not null)
            return null;

        var imageUrl = await _cloudinaryService.UploadImageAsync(dto.Image);
        var president = new President(imageUrl, dto.Text);

        await _repository.AddAsync(president);
        await _repository.SaveChangesAsync();

        return MapToDto(president);
    }

    public async Task<PresidentResponseDto?> UpdateAsync(UpdatePresidentDto dto)
    {
        var president = await _repository.GetSingleAsync();
        if (president is null)
            return null;

        // Yeni şəkil göndərilibsə — Cloudinary-yə yüklə, yoxsa null (entity köhnəni saxlayır)
        string? imageUrl = null;
        if (dto.Image is not null)
            imageUrl = await _cloudinaryService.UploadImageAsync(dto.Image);

        president.Update(imageUrl, dto.Text);
        _repository.Update(president);
        await _repository.SaveChangesAsync();

        return MapToDto(president);
    }

    public async Task<bool> DeleteAsync()
    {
        var president = await _repository.GetSingleAsync();
        if (president is null)
            return false;

        await _repository.DeleteAsync(president.Id);
        await _repository.SaveChangesAsync();
        return true;
    }

    private static PresidentResponseDto MapToDto(President president)
        => new(president.Id, president.ImageUrl, president.Text);
}
