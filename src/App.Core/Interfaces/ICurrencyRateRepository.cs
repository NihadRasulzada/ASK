using App.Core.Entities;

namespace App.Core.Interfaces;

public interface ICurrencyRateRepository : IRepository<CurrencyRate>
{
    Task<IEnumerable<CurrencyRate>> GetByDateAsync(DateOnly date);
    Task<CurrencyRate?> GetByCodeAndDateAsync(string code, DateOnly date);
    Task DeleteOlderThanAsync(DateOnly cutoff);
}
