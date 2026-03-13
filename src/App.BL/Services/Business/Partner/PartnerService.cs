using App.BL.DTOs;
using App.BL.Mapper.Partner;
using App.BL.Services.External;
using App.Core.Interfaces.Repository.Partner;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.Partner;

public class PartnerService(
    IPartnerReadRepository readRepository,
    IPartnerWriteRepository writeRepository,
    ICloudinaryService cloudinaryService,
    IPartnerMapper mapper) : IPartnerService
{
    public async Task<Response> CreateAsync(CreatePartnerDto dto, CancellationToken cancellationToken = default)
    {
        string imageUrl = await cloudinaryService.UploadImageAsync(dto.Image);

        Core.Entities.Partner entity = mapper.CreateDtoToDomain(dto, imageUrl);

        await writeRepository.AddAsync(entity, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response.Success("Partner created successfully");
    }

    public async Task<Response> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.Partner? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
            return Response.NotFound("Partner not found");

        await writeRepository.HardDeleteAsync(id, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response.Success("Partner deleted successfully");
    }

    public async Task<Response<IEnumerable<PartnerResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<Core.Entities.Partner> entities = await readRepository.GetAllAsync(false, cancellationToken);

        if (!entities.Any())
            return Response<IEnumerable<PartnerResponseDto>>
                .Success(Enumerable.Empty<PartnerResponseDto>(), "No partners found");

        IEnumerable<PartnerResponseDto> result = entities.Select(x => mapper.DomainToResponseDto(x));

        return Response<IEnumerable<PartnerResponseDto>>
            .Success(result, $"{result.Count()} partners retrieved successfully");
    }

    public async Task<Response<PartnerResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Core.Entities.Partner? entity = await readRepository.GetByIdAsync(id, false, cancellationToken);

        if (entity == null)
            return Response<PartnerResponseDto?>.NotFound("Partner not found");

        PartnerResponseDto dto = mapper.DomainToResponseDto(entity);

        return Response<PartnerResponseDto?>.Success(dto, "Partner retrieved successfully");
    }

    public async Task<Response<PartnerResponseDto?>> UpdateAsync(Guid id, UpdatePartnerDto dto, CancellationToken cancellationToken = default)
    {
        Core.Entities.Partner? entity = await readRepository.GetByIdAsync(id, true, cancellationToken);

        if (entity == null)
            return Response<PartnerResponseDto?>.NotFound("Partner not found");

        string? newImageUrl = null;
        if (dto.Image != null)
        {
            newImageUrl = await cloudinaryService.UploadImageAsync(dto.Image);
        }

        mapper.UpdateDtoToDomain(entity, dto, newImageUrl);

        writeRepository.Update(entity);
        await writeRepository.SaveChangesAsync(cancellationToken);

        PartnerResponseDto response = mapper.DomainToResponseDto(entity);

        return Response<PartnerResponseDto?>.Success(response, "Partner updated successfully");
    }
}
