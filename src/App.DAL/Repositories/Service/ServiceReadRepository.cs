using App.Core.Interfaces.Repository.Service;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.Service;

public class ServiceReadRepository : SoftDeletableReadRepository<Core.Entities.Service>, IServiceReadRepository
{
    public ServiceReadRepository(AppDbContext context) : base(context)
    {
    }
}