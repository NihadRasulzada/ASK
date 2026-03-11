using App.Core.Interfaces.Repository.Partner;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.Partner;

public class PartnerReadRepository : ReadRepository<Core.Entities.Partner>, IPartnerReadRepository
{
    public PartnerReadRepository(AppDbContext context) : base(context)
    {
    }
}