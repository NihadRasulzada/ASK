using App.Core.Interfaces.Repository.FAQInquiry;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.FAQInquiry;

public class FAQInquiryWriteRepository(AppDbContext context, IFAQInquiryReadRepository readRepository)
    : WriteRepository<Core.Entities.FAQInquiry>(context, readRepository), IFAQInquiryWriteRepository
{
}
