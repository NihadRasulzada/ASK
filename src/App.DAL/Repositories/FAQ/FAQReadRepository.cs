using App.Core.Interfaces.Repository.FAQ;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.FAQ;

public class FAQReadRepository(AppDbContext context)
    : SoftDeletableReadRepository<Core.Entities.FAQ>(context), IFAQReadRepository
{
}
