using App.Core.Interfaces.Repository.Management;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.Management;

public class ManagementWriteRepository : WriteRepository<Core.Entities.Management>, IManagementWriteRepository
{
    public ManagementWriteRepository(AppDbContext context, IManagementReadRepository readRepository)
        : base(context, readRepository)
    {
    }
}
