using App.Core.Interfaces.Repository.Presidium;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.Presidium;

public class PresidiumReadRepository : ReadRepository<Core.Entities.Presidium>, IPresidiumReadRepository
{
    public PresidiumReadRepository(AppDbContext context) : base(context)
    {
    }
}
