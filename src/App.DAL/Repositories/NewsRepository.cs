using App.Core.Entities;
using App.Core.Interfaces;
using App.DAL.Context;

namespace App.DAL.Repositories;

public class NewsRepository : Repository<News>, INewsRepository
{
    public NewsRepository(AppDbContext context) : base(context)
    {
    }
}
