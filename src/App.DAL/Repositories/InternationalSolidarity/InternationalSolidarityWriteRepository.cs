using App.Core.Interfaces.Repository.InternationalSolidarity;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.InternationalSolidarity;

public class InternationalSolidarityWriteRepository : WriteRepository<Core.Entities.InternationalSolidarity>, IInternationalSolidarityWriteRepository
{
    public InternationalSolidarityWriteRepository(AppDbContext context, IInternationalSolidarityReadRepository readRepository)
        : base(context, readRepository)
    {
    }
}
