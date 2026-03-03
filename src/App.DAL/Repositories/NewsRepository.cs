using App.Core.Entities;
using App.Core.Interfaces;
using App.DAL.Context;

namespace App.DAL.Repositories;

public class NewsRepository : AuditableRepository<News>, INewsRepository
{
    public NewsRepository(AppDbContext context) : base(context)
    {
    }
}
