using App.Core.Interfaces.Repository.Common;

namespace App.Core.Interfaces.Repository.Service;

public interface IServiceReadRepository : ISoftDeletableReadRepository<Entities.Service>
{
}