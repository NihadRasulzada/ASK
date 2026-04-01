using App.Core.Interfaces.Repository.InternationalSolidarity;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.InternationalSolidarity;

public class InternationalSolidarityReadRepository : ReadRepository<Core.Entities.InternationalSolidarity>, IInternationalSolidarityReadRepository
{
    public InternationalSolidarityReadRepository(AppDbContext context) : base(context)
    {
    }
}
