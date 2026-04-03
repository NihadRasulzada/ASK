using App.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Entities;

public class FAQ : SoftDeletableEntity
{
    public string QuestionAz { get; set; }
    public string QuestionEn { get; set; }
    public string QuestionRu { get; set; }
    public string AnswerAz { get; set; }
    public string AnswerEn { get; set; }
    public string AnswerRu { get; set; }
    public FAQ(Guid id, bool isDeactive) : base(id, isDeactive)
    {
    }
}
