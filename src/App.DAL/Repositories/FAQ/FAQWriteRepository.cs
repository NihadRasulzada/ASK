using App.Core.Interfaces.Repository.FAQ;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.FAQ;

public class FAQWriteRepository(AppDbContext context, IFAQReadRepository readRepository)
    : SoftDeletableWriteRepository<Core.Entities.FAQ>(context, readRepository), IFAQWriteRepository
{
}
