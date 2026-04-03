using App.BL.DTOs;
using App.BL.Mapper.FAQ;
using App.Core.Interfaces.Repository.FAQ;
using App.Core.Interfaces.Repository.FAQInquiry;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.FAQ;

public class FAQService(
    IFAQReadRepository readRepository,
    IFAQWriteRepository writeRepository,
    IFAQInquiryReadRepository inquiryReadRepository,
    IFAQInquiryWriteRepository inquiryWriteRepository,
    IFAQMapper mapper) : IFAQService
{
    public async Task<PagedResponse<IEnumerable<FAQResponseDto>>> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
    {
        var (items, totalCount) = await readRepository.GetPagedAsync(false, false, pageIndex, pageSize, cancellationToken);
        var result = items.Select(mapper.DomainToResponseDto);
        return PagedResponse<IEnumerable<FAQResponseDto>>.Create(result, pageIndex, pageSize, totalCount, "FAQs retrieved successfully");
    }

    public async Task<PagedResponse<IEnumerable<FAQResponseDto>>> GetAllIncludingDeletedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
    {
        var (items, totalCount) = await readRepository.GetPagedAsync(false, true, pageIndex, pageSize, cancellationToken);
        var result = items.Select(mapper.DomainToResponseDto);
        return PagedResponse<IEnumerable<FAQResponseDto>>.Create(result, pageIndex, pageSize, totalCount, "All FAQs retrieved successfully");
    }

    public async Task<Response<FAQResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await readRepository.GetByIdIncludingDeletedAsync(id, false, cancellationToken);
        if (entity is null) return Response<FAQResponseDto?>.NotFound("FAQ not found");

        return Response<FAQResponseDto?>.Success(mapper.DomainToResponseDto(entity), "FAQ retrieved successfully");
    }

    public async Task<Response<FAQResponseDto>> CreateAsync(CreateFAQDto dto, CancellationToken cancellationToken = default)
    {
        var entity = mapper.CreateDtoToDomain(dto);
        await writeRepository.AddAsync(entity, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<FAQResponseDto>.Success(mapper.DomainToResponseDto(entity), "FAQ created successfully");
    }

    public async Task<Response<FAQResponseDto?>> UpdateAsync(Guid id, UpdateFAQDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await readRepository.GetByIdAsync(id, true, cancellationToken);
        if (entity is null) return Response<FAQResponseDto?>.NotFound("FAQ not found");

        mapper.UpdateDtoToDomain(entity, dto);
        writeRepository.Update(entity);
        await writeRepository.SaveChangesAsync(cancellationToken);

        return Response<FAQResponseDto?>.Success(mapper.DomainToResponseDto(entity), "FAQ updated successfully");
    }

    public async Task<Response> ActivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await readRepository.GetByIdIncludingDeletedAsync(id, true, cancellationToken);
        if (entity is null) return Response.NotFound("FAQ not found");
        if (!entity.IsDeactive) return Response.BadRequest("FAQ is already active");

        entity.Activate();
        await writeRepository.SaveChangesAsync(cancellationToken);
        return Response.Success("FAQ activated successfully");
    }

    public async Task<Response> DeActivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await readRepository.GetByIdAsync(id, true, cancellationToken);
        if (entity is null) return Response.NotFound("FAQ not found");
        if (entity.IsDeactive) return Response.BadRequest("FAQ is already deactive");

        entity.Deactivate();
        await writeRepository.SaveChangesAsync(cancellationToken);
        return Response.Success("FAQ deactivated successfully");
    }

    public async Task<Response> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await readRepository.GetByIdIncludingDeletedAsync(id, true, cancellationToken);
        if (entity is null) return Response.NotFound("FAQ not found");

        await writeRepository.HardDeleteIncludingDeletedAsync(id, cancellationToken);
        await writeRepository.SaveChangesAsync(cancellationToken);
        return Response.Success("FAQ deleted successfully");
    }

    public async Task<Response<FAQInquiryResponseDto>> SubmitInquiryAsync(SubmitFAQInquiryDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new Core.Entities.FAQInquiry(dto.Question);
        await inquiryWriteRepository.AddAsync(entity, cancellationToken);
        await inquiryWriteRepository.SaveChangesAsync(cancellationToken);

        return Response<FAQInquiryResponseDto>.Success(mapper.InquiryDomainToResponseDto(entity), "Inquiry submitted successfully");
    }

    public async Task<PagedResponse<IEnumerable<FAQInquiryResponseDto>>> GetInquiriesAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
    {
        var (items, totalCount) = await inquiryReadRepository.GetPagedAsync(false, false, pageIndex, pageSize, cancellationToken);
        var result = items.Select(mapper.InquiryDomainToResponseDto);
        return PagedResponse<IEnumerable<FAQInquiryResponseDto>>.Create(result, pageIndex, pageSize, totalCount, "Inquiries retrieved successfully");
    }
}
