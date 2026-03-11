using App.Core.Interfaces.Repository.Common;

namespace App.Core.Interfaces.Repository.News;

public interface INewsWriteRepository : ISoftDeletableWriteRepository<Entities.News>
{
}