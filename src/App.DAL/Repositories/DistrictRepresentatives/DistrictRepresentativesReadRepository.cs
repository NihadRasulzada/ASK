using App.Core.Interfaces.Repository.DistrictRepresentatives;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.DistrictRepresentatives;

public class DistrictRepresentativesReadRepository : ReadRepository<Core.Entities.DistrictRepresentatives>, IDistrictRepresentativesReadRepository
{
    public DistrictRepresentativesReadRepository(AppDbContext context) : base(context)
    {
    }
}
