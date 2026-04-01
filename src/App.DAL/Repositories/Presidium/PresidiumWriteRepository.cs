using App.Core.Interfaces.Repository.Presidium;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.Presidium;

public class PresidiumWriteRepository : WriteRepository<Core.Entities.Presidium>, IPresidiumWriteRepository
{
    public PresidiumWriteRepository(AppDbContext context, IPresidiumReadRepository readRepository)
        : base(context, readRepository)
    {
    }
}
