using App.Core.Interfaces.Repository.ForeignRepresentatives;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.ForeignRepresentatives;

public class ForeignRepresentativesReadRepository : ReadRepository<Core.Entities.ForeignRepresentatives>, IForeignRepresentativesReadRepository
{
    public ForeignRepresentativesReadRepository(AppDbContext context) : base(context)
    {
    }
}
