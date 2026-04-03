using App.Core.Interfaces.Repository.BusinessForum;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.BusinessForum;

public class BusinessForumReadRepository(AppDbContext context)
    : ReadRepository<Core.Entities.BusinessForum>(context), IBusinessForumReadRepository
{
}
