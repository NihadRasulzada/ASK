using App.Core.Interfaces.Repository.Common;

namespace App.Core.Interfaces.Repository.UserPeriodSetting;

public interface IUserPeriodSettingReadRepository
    : IReadRepository<Core.Entities.UserPeriodSetting>
{
    Task<Core.Entities.UserPeriodSetting?> GetCurrentAsync(
        CancellationToken cancellationToken = default);
}
