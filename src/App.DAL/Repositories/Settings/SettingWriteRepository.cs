using App.Core.Interfaces.Repository.Settings;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.Settings;

public class SettingWriteRepository : WriteRepository<Core.Entities.Setting>, ISettingWriteRepository
{
    public SettingWriteRepository(AppDbContext context, ISettingReadRepository readRepository)
        : base(context, readRepository)
    {
    }
}
