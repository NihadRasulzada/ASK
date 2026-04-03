using App.Core.Interfaces.Repository.FAQInquiry;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.FAQInquiry;

public class FAQInquiryReadRepository(AppDbContext context)
    : ReadRepository<Core.Entities.FAQInquiry>(context), IFAQInquiryReadRepository
{
}
