using App.Core.Interfaces.Repository.Settings;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.Settings;

public class SettingReadRepository : ReadRepository<Core.Entities.Setting>, ISettingReadRepository
{
    public SettingReadRepository(AppDbContext context) : base(context)
    {
    }
}
