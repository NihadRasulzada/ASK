using App.Core.Interfaces.Repository.ForeignRepresentatives;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.ForeignRepresentatives;

public class ForeignRepresentativesWriteRepository : WriteRepository<Core.Entities.ForeignRepresentatives>, IForeignRepresentativesWriteRepository
{
    public ForeignRepresentativesWriteRepository(AppDbContext context, IForeignRepresentativesReadRepository readRepository)
        : base(context, readRepository)
    {
    }
}
