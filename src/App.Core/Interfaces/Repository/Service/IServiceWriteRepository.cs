using App.Core.Interfaces.Repository.Common;

namespace App.Core.Interfaces.Repository.Service;

public interface IServiceWriteRepository : ISoftDeletableWriteRepository<Entities.Service>
{
}