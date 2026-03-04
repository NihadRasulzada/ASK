using App.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.BL.Services.Abstractions;

public interface IAnnouncementService
{
    Task<IEnumerable<AnnouncementResponseDto>> GetAllAsync();
    Task<AnnouncementResponseDto?> GetByIdAsync(Guid id);
    Task<AnnouncementResponseDto> CreateAsync(CreateAnnouncementDto dto);
    Task<AnnouncementResponseDto?> UpdateAsync(Guid id, UpdateAnnouncementDto dto);
    Task<bool> DeleteAsync(Guid id);
}