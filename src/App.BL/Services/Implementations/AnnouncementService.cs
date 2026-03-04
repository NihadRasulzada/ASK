using App.BL.DTOs;
using App.BL.Services.Abstractions;
using App.Core.Entities;
using App.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.BL.Services.Implementations;

public class AnnouncementService : IAnnouncementService
{
    private readonly IAnnouncementRepository _repository;
    private readonly ICloudinaryService _cloudinaryService;

    public AnnouncementService(IAnnouncementRepository repository, ICloudinaryService cloudinaryService)
    {
        _repository = repository;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<IEnumerable<AnnouncementResponseDto>> GetAllAsync()
    {
        var Announcements = await _repository.GetAllAsync();
        return Announcements.Select(MapToDto);
    }

    public async Task<AnnouncementResponseDto?> GetByIdAsync(Guid id)
    {
        var Announcement = await _repository.GetByIdAsync(id);
        return Announcement is null ? null : MapToDto(Announcement);
    }

    public async Task<AnnouncementResponseDto> CreateAsync(CreateAnnouncementDto dto)
    {
        var imageUrl = await _cloudinaryService.UploadImageAsync(dto.TitleImage);
        var Announcement = new Announcement(dto.Title,imageUrl, dto.Text);

        await _repository.AddAsync(Announcement);
        await _repository.SaveChangesAsync();

        return MapToDto(Announcement);
    }

    public async Task<AnnouncementResponseDto?> UpdateAsync(Guid id, UpdateAnnouncementDto dto)
    {
        var Announcement = await _repository.GetByIdAsync(id);
        if (Announcement is null)
            return null;

        // Yeni şəkil göndərilibsə — Cloudinary-yə yüklə, yoxsa null (entity köhnəni saxlayır)
        string? imageUrl = null;
        if (dto.TitleImage is not null)
            imageUrl = await _cloudinaryService.UploadImageAsync(dto.TitleImage);

        Announcement.Update(dto.Title,imageUrl, dto.Text);
        _repository.Update(Announcement);
        await _repository.SaveChangesAsync();

        return MapToDto(Announcement);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var Announcement = await _repository.GetByIdAsync(id);
        if (Announcement is null)
            return false;

        await _repository.DeleteAsync(id);
        await _repository.SaveChangesAsync();
        return true;
    }

    private static AnnouncementResponseDto MapToDto(Announcement Announcement)
        => new(Announcement.Id, Announcement.Title, Announcement.TitleImageUrl, Announcement.Text);
}
