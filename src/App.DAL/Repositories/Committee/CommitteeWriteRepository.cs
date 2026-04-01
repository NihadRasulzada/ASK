using App.Core.Interfaces.Repository.Committee;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.Committee;

public class CommitteeWriteRepository : WriteRepository<Core.Entities.Committee>, ICommitteeWriteRepository
{
    public CommitteeWriteRepository(AppDbContext context, ICommitteeReadRepository readRepository)
        : base(context, readRepository)
    {
    }
}
