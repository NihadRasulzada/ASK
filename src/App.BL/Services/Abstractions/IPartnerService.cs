using App.BL.DTOs;

namespace App.BL.Services.Abstractions;

public interface IPartnerService
{
    Task<IEnumerable<PartnerResponseDto>> GetAllAsync();
    Task<PartnerResponseDto?> GetByIdAsync(Guid id);
    Task<PartnerResponseDto> CreateAsync(CreatePartnerDto dto);
    Task<PartnerResponseDto?> UpdateAsync(Guid id, UpdatePartnerDto dto);
    Task<bool> DeleteAsync(Guid id);
}
