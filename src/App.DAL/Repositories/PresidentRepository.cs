using App.Core.Entities;
using App.Core.Interfaces;
using App.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.Repositories;

public class PresidentRepository : Repository<President>, IPresidentRepository
{
    public PresidentRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<President?> GetSingleAsync()
        => await _dbSet.FirstOrDefaultAsync();
}
