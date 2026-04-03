using App.Core.Entities.Common;

namespace App.Core.Entities;

public class FAQInquiry : BaseEntity
{
    public string Question { get; private set; }
    public DateTime SubmittedAt { get; private set; }

    private FAQInquiry() : base(Guid.Empty) { }

    public FAQInquiry(string question) : base(Guid.NewGuid())
    {
        Question = question;
        SubmittedAt = DateTime.UtcNow;
    }
}
