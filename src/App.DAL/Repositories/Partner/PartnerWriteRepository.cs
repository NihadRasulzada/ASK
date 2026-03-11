using App.Core.Interfaces.Repository.Common;
using App.Core.Interfaces.Repository.Partner;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.Partner;

public class PartnerWriteRepository : WriteRepository<Core.Entities.Partner>, IPartnerWriteRepository
{
    public PartnerWriteRepository(AppDbContext context, IReadRepository<Core.Entities.Partner> readRepository) : base(context, readRepository)
    {
    }
}
