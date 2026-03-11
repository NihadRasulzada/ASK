using App.Core.Entities;
using App.Core.Interfaces.Repository.News;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.News;

public class NewsReadRepository : SoftDeletableReadRepository<Core.Entities.News>, INewsReadRepository
{
    public NewsReadRepository(AppDbContext context) : base(context)
    {
    }
}