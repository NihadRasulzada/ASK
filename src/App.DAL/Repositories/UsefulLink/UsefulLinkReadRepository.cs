using App.Core.Interfaces.Repository.UsefulLink;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.UsefulLink;

public class UsefulLinkReadRepository(AppDbContext context)
    : SoftDeletableReadRepository<Core.Entities.UsefulLink>(context), IUsefulLinkReadRepository
{
}
