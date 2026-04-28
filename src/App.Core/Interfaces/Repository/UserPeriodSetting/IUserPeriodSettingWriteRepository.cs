using System;
using System.Collections.Generic;
using System.Text;
using App.Core.Interfaces.Repository.Common;

namespace App.Core.Interfaces.Repository.UserPeriodSetting;

public interface IUserPeriodSettingWriteRepository
    : IWriteRepository<Core.Entities.UserPeriodSetting>
{
}
