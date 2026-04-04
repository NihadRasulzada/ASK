using App.BL.DTOs;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.FAQ;

public interface IFAQService
{
    Task<Response<IEnumerable<FAQResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Response<IEnumerable<FAQResponseDto>>> GetAllIncludingDeletedAsync(CancellationToken cancellationToken = default);
    Task<Response<FAQResponseDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response<FAQResponseDto>> CreateAsync(CreateFAQDto dto, CancellationToken cancellationToken = default);
    Task<Response<FAQResponseDto?>> UpdateAsync(Guid id, UpdateFAQDto dto, CancellationToken cancellationToken = default);
    Task<Response> ActivateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response> DeActivateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response<FAQInquiryResponseDto>> SubmitInquiryAsync(SubmitFAQInquiryDto dto, CancellationToken cancellationToken = default);
    Task<PagedResponse<IEnumerable<FAQInquiryResponseDto>>> GetInquiriesAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);
}
