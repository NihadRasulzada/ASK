using App.Core.Entities.Common;

namespace App.Core.Entities;

public class FAQ : SoftDeletableEntity
{
    public string QuestionAz { get; private set; }
    public string QuestionEn { get; private set; }
    public string QuestionRu { get; private set; }
    public string AnswerAz { get; private set; }
    public string AnswerEn { get; private set; }
    public string AnswerRu { get; private set; }

    private FAQ() : base(Guid.Empty, false) { }

    public FAQ(string questionAz, string questionEn, string questionRu,
               string answerAz, string answerEn, string answerRu)
        : base(Guid.NewGuid(), false)
    {
        QuestionAz = questionAz;
        QuestionEn = questionEn;
        QuestionRu = questionRu;
        AnswerAz = answerAz;
        AnswerEn = answerEn;
        AnswerRu = answerRu;
    }

    public void Update(string questionAz, string questionEn, string questionRu,
                       string answerAz, string answerEn, string answerRu)
    {
        QuestionAz = questionAz;
        QuestionEn = questionEn;
        QuestionRu = questionRu;
        AnswerAz = answerAz;
        AnswerEn = answerEn;
        AnswerRu = answerRu;
    }
}
