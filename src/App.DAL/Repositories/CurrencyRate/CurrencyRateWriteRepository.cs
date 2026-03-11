using App.Core.Interfaces.Repository.Common;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.CurrencyRate;

public class CurrencyRateWriteRepository : WriteRepository<Core.Entities.CurrencyRate>, IWriteRepository<Core.Entities.CurrencyRate>
{
    public CurrencyRateWriteRepository(AppDbContext context, IReadRepository<Core.Entities.CurrencyRate> readRepository) : base(context, readRepository)
    {
    }
}