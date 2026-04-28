using App.Core.Interfaces.Repository.UserPeriodSetting;
using App.DAL.Context;
using App.DAL.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace App.DAL.Repositories.UserPeriodSetting;

public class UserPeriodSettingReadRepository(AppDbContext context)
    : ReadRepository<Core.Entities.UserPeriodSetting>(context),
      IUserPeriodSettingReadRepository
{
    public async Task<Core.Entities.UserPeriodSetting?> GetCurrentAsync(
        CancellationToken cancellationToken = default)
    {
        return await Table
            .OrderByDescending(x => x.UpdatedAt)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
