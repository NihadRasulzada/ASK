using App.BL.DTOs;
using App.Core.Interfaces;

namespace App.BL.Mapper.FAQ;

public class FAQMapper(ILanguageService languageService) : IFAQMapper
{
    public Core.Entities.FAQ CreateDtoToDomain(CreateFAQDto dto)
    {
        return new Core.Entities.FAQ(
            dto.QuestionAz, dto.QuestionEn, dto.QuestionRu,
            dto.AnswerAz, dto.AnswerEn, dto.AnswerRu);
    }

    public FAQResponseDto DomainToResponseDto(Core.Entities.FAQ entity)
    {
        var question = languageService.Lang switch
        {
            "az" => entity.QuestionAz,
            "en" => entity.QuestionEn,
            "ru" => entity.QuestionRu,
            _ => entity.QuestionAz
        };

        var answer = languageService.Lang switch
        {
            "az" => entity.AnswerAz,
            "en" => entity.AnswerEn,
            "ru" => entity.AnswerRu,
            _ => entity.AnswerAz
        };

        return new FAQResponseDto(entity.Id, question, answer, entity.IsDeactive);
    }

    public FAQInquiryResponseDto InquiryDomainToResponseDto(Core.Entities.FAQInquiry entity)
    {
        return new FAQInquiryResponseDto(entity.Id, entity.Question, entity.SubmittedAt);
    }

    public Core.Entities.FAQ UpdateDtoToDomain(Core.Entities.FAQ entity, UpdateFAQDto dto)
    {
        entity.Update(
            dto.QuestionAz, dto.QuestionEn, dto.QuestionRu,
            dto.AnswerAz, dto.AnswerEn, dto.AnswerRu);
        return entity;
    }
}
