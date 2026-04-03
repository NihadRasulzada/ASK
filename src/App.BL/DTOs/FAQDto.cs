namespace App.BL.DTOs;

public class CreateFAQDto
{
    public string QuestionAz { get; set; }
    public string QuestionEn { get; set; }
    public string QuestionRu { get; set; }
    public string AnswerAz { get; set; }
    public string AnswerEn { get; set; }
    public string AnswerRu { get; set; }
}

public class UpdateFAQDto
{
    public string QuestionAz { get; set; }
    public string QuestionEn { get; set; }
    public string QuestionRu { get; set; }
    public string AnswerAz { get; set; }
    public string AnswerEn { get; set; }
    public string AnswerRu { get; set; }
}

public record FAQResponseDto(Guid Id, string Question, string Answer, bool IsDeactive);

public class SubmitFAQInquiryDto
{
    public string Question { get; set; }
}

public record FAQInquiryResponseDto(Guid Id, string Question, DateTime SubmittedAt);
