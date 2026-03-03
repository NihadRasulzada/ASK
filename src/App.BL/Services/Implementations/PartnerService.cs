using App.BL.DTOs;
using App.BL.Services.Abstractions;
using App.Core.Entities;
using App.Core.Interfaces;

namespace App.BL.Services.Implementations;

public class PartnerService : IPartnerService
{
    private readonly IPartnerRepository _partnerRepository;
    private readonly ICloudinaryService _cloudinaryService;

    public PartnerService(IPartnerRepository partnerRepository, ICloudinaryService cloudinaryService)
    {
        _partnerRepository = partnerRepository;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<IEnumerable<PartnerResponseDto>> GetAllAsync()
    {
        var partners = await _partnerRepository.GetAllAsync();
        return partners.Select(MapToResponseDto);
    }

    public async Task<PartnerResponseDto?> GetByIdAsync(Guid id)
    {
        var partner = await _partnerRepository.GetByIdAsync(id);
        return partner is null ? null : MapToResponseDto(partner);
    }

    public async Task<PartnerResponseDto> CreateAsync(CreatePartnerDto dto)
    {
        var imageUrl = await _cloudinaryService.UploadImageAsync(dto.Image);

        var partner = new Partner(imageUrl, dto.Site);
        await _partnerRepository.AddAsync(partner);
        await _partnerRepository.SaveChangesAsync();

        return MapToResponseDto(partner);
    }

    public async Task<PartnerResponseDto?> UpdateAsync(Guid id, UpdatePartnerDto dto)
    {
        var partner = await _partnerRepository.GetByIdAsync(id);
        if (partner is null)
            return null;

        string? imageUrl = null;
        if (dto.Image is not null)
            imageUrl = await _cloudinaryService.UploadImageAsync(dto.Image);

        partner.Update(imageUrl, dto.Site);
        _partnerRepository.Update(partner);
        await _partnerRepository.SaveChangesAsync();

        return MapToResponseDto(partner);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var partner = await _partnerRepository.GetByIdAsync(id);
        if (partner is null)
            return false;

        await _partnerRepository.DeleteAsync(id);
        await _partnerRepository.SaveChangesAsync();
        return true;
    }

    private static PartnerResponseDto MapToResponseDto(Partner partner)
        => new(partner.Id, partner.ImageUrl, partner.Site);
}
