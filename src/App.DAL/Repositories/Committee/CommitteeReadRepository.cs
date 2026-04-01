using App.Core.Interfaces.Repository.Committee;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.Committee;

public class CommitteeReadRepository : ReadRepository<Core.Entities.Committee>, ICommitteeReadRepository
{
    public CommitteeReadRepository(AppDbContext context) : base(context)
    {
    }
}
