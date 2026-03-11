using App.Core.Interfaces.Repository.Common;

namespace App.Core.Interfaces.Repository.News;

public interface INewsReadRepository : ISoftDeletableReadRepository<Entities.News>
{
}