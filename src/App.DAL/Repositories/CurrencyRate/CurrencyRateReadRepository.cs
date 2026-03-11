using App.Core.Entities;
using App.Core.Interfaces.Repository.CurrencyRate;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.CurrencyRate;

public class CurrencyRateReadRepository : ReadRepository<Core.Entities.CurrencyRate>, ICurrencyRateReadRepository
{
    public CurrencyRateReadRepository(AppDbContext context) : base(context)
    {
    }
}