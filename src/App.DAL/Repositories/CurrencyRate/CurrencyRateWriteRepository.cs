using App.Core.Interfaces.Repository.CurrencyRate;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.CurrencyRate;

public class CurrencyRateWriteRepository(AppDbContext context, ICurrencyRateReadRepository readRepository)
    : WriteRepository<Core.Entities.CurrencyRate>(context, readRepository), ICurrencyRateWriteRepository
{
}