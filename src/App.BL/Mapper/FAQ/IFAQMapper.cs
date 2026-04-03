using App.BL.DTOs;

namespace App.BL.Mapper.FAQ;

public interface IFAQMapper
{
    Core.Entities.FAQ CreateDtoToDomain(CreateFAQDto dto);
    Core.Entities.FAQ UpdateDtoToDomain(Core.Entities.FAQ entity, UpdateFAQDto dto);
    FAQResponseDto DomainToResponseDto(Core.Entities.FAQ entity);
    FAQInquiryResponseDto InquiryDomainToResponseDto(Core.Entities.FAQInquiry entity);
}
