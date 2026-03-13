using App.Core.Interfaces.Repository.Common;
using App.Core.Interfaces.Repository.NewsImage;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.NewsImage;

public class NewsImageWriteRepository : WriteRepository<Core.Entities.NewsImage>, INewsImageWriteRepository
{
    public NewsImageWriteRepository(AppDbContext context, IReadRepository<Core.Entities.NewsImage> readRepository) : base(context, readRepository)
    {
    }
}