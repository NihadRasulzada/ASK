using System;
using System.Collections.Generic;
using System.Text;
using App.BL.DTOs;
using App.Core.ResponseObject.Concreate;

namespace App.BL.Services.Business.Calendar;

public interface ICalendarService
{
    Task<Response<CalendarResponseDto>> GetByDateAsync(DateTime date, CancellationToken cancellationToken = default);
}
