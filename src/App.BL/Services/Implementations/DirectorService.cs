using App.BL.DTOs;
using App.BL.Services.Abstractions;
using App.Core.Entities;
using App.Core.Interfaces;

namespace App.BL.Services.Implementations;

public class DirectorService : IDirectorService
{
    private readonly IDirectorRepository _repository;
    private readonly ICloudinaryService _cloudinaryService;

    public DirectorService(IDirectorRepository repository, ICloudinaryService cloudinaryService)
    {
        _repository = repository;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<IEnumerable<DirectorResponseDto>> GetAllAsync()
    {
        var directors = await _repository.GetAllAsync();
        return directors.Select(MapToDto);
    }

    public async Task<DirectorResponseDto?> GetByIdAsync(Guid id)
    {
        var director = await _repository.GetByIdAsync(id);
        return director is null ? null : MapToDto(director);
    }

    public async Task<DirectorResponseDto> CreateAsync(CreateDirectorDto dto)
    {
        var imageUrl = await _cloudinaryService.UploadImageAsync(dto.Image);
        var director = new Director(imageUrl, dto.FullName, dto.Duty);

        await _repository.AddAsync(director);
        await _repository.SaveChangesAsync();

        return MapToDto(director);
    }

    public async Task<DirectorResponseDto?> UpdateAsync(Guid id, UpdateDirectorDto dto)
    {
        var director = await _repository.GetByIdAsync(id);
        if (director is null)
            return null;

        // Yeni şəkil göndərilibsə — Cloudinary-yə yüklə, yoxsa null (entity köhnəni saxlayır)
        string? imageUrl = null;
        if (dto.Image is not null)
            imageUrl = await _cloudinaryService.UploadImageAsync(dto.Image);

        director.Update(imageUrl, dto.FullName, dto.Duty);
        _repository.Update(director);
        await _repository.SaveChangesAsync();

        return MapToDto(director);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var director = await _repository.GetByIdAsync(id);
        if (director is null)
            return false;

        await _repository.DeleteAsync(id);
        await _repository.SaveChangesAsync();
        return true;
    }

    private static DirectorResponseDto MapToDto(Director director)
        => new(director.Id, director.ImageUrl, director.FullName, director.Duty);
}
