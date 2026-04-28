using System;
using System.Collections.Generic;
using System.Text;
using App.Core.Interfaces.Repository.Common;
using App.Core.Interfaces.Repository.UserPeriodSetting;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.UserPeriodSetting;

public class UserPeriodSettingWriteRepository(
    AppDbContext context,
    IReadRepository<Core.Entities.UserPeriodSetting> readRepository)
    : WriteRepository<Core.Entities.UserPeriodSetting>(context, readRepository),
      IUserPeriodSettingWriteRepository
{
}

