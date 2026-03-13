using App.Core.Interfaces.Repository.Common;
using App.Core.Interfaces.Repository.Service;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.Service;

public class ServiceWriteRepository : SoftDeletableWriteRepository<Core.Entities.Service>, IServiceWriteRepository
{
    public ServiceWriteRepository(AppDbContext context, IServiceReadRepository readRepository) : base(context, readRepository)
    {
    }
}