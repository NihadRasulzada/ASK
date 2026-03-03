using App.Core.Entities;
using App.Core.Interfaces;
using App.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.Repositories;

public class CurrencyRateRepository : Repository<CurrencyRate>, ICurrencyRateRepository
{
    public CurrencyRateRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<CurrencyRate>> GetByDateAsync(DateOnly date)
        => await _dbSet.Where(r => r.RateDate == date).ToListAsync();

    public async Task<CurrencyRate?> GetByCodeAndDateAsync(string code, DateOnly date)
        => await _dbSet.FirstOrDefaultAsync(r => r.CurrencyCode == code && r.RateDate == date);

    public async Task DeleteOlderThanAsync(DateOnly cutoff)
    {
        var old = await _dbSet.Where(r => r.RateDate < cutoff).ToListAsync();
        _dbSet.RemoveRange(old);
    }
}
