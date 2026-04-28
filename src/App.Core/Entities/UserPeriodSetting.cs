using System;
using System.Collections.Generic;
using System.Text;
using App.Core.Entities.Common;
using App.Core.Enums;

namespace App.Core.Entities;

public class UserPeriodSetting : BaseEntity
{
    public RatePeriod SelectedPeriod { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private UserPeriodSetting() : base(Guid.NewGuid()) { }

    public UserPeriodSetting(RatePeriod period) : base(Guid.NewGuid())
    {
        SelectedPeriod = period;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdatePeriod(RatePeriod period)
    {
        SelectedPeriod = period;
        UpdatedAt = DateTime.UtcNow;
    }
}
