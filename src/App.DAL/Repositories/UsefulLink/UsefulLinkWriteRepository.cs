using App.Core.Interfaces.Repository.UsefulLink;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.UsefulLink;

public class UsefulLinkWriteRepository(AppDbContext context, IUsefulLinkReadRepository readRepository)
    : SoftDeletableWriteRepository<Core.Entities.UsefulLink>(context, readRepository), IUsefulLinkWriteRepository
{
}
