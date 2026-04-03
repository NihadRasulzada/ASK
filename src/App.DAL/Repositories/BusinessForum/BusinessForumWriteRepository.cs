using App.Core.Interfaces.Repository.BusinessForum;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.BusinessForum;

public class BusinessForumWriteRepository(AppDbContext context, IBusinessForumReadRepository readRepository)
    : WriteRepository<Core.Entities.BusinessForum>(context, readRepository), IBusinessForumWriteRepository
{
}
