using App.Core.Interfaces.Repository.DistrictRepresentatives;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.DistrictRepresentatives;

public class DistrictRepresentativesWriteRepository : WriteRepository<Core.Entities.DistrictRepresentatives>, IDistrictRepresentativesWriteRepository
{
    public DistrictRepresentativesWriteRepository(AppDbContext context, IDistrictRepresentativesReadRepository readRepository)
        : base(context, readRepository)
    {
    }
}
