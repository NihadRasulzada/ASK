using App.Core.Interfaces.Repository.Common;
using App.Core.Interfaces.Repository.News;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.News;

public class NewsWriteRepository : SoftDeletableWriteRepository<Core.Entities.News>, INewsWriteRepository
{
    public NewsWriteRepository(AppDbContext context, ISoftDeletableReadRepository<Core.Entities.News> softDeletableReadRepository) : base(context, softDeletableReadRepository)
    {
    }
}