using App.Core.Interfaces.Repository.Management;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.Management;

public class ManagementReadRepository : ReadRepository<Core.Entities.Management>, IManagementReadRepository
{
    public ManagementReadRepository(AppDbContext context) : base(context)
    {
    }
}
