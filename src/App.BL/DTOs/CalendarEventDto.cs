using System;
using System.Collections.Generic;
using System.Text;

namespace App.BL.DTOs;

public record CalendarEventDto(
    Guid Id,
    string EventType,
    string TitleAz,
    string TitleEn,
    string TitleRu,
    string TitleImageUrl,
    DateTime StartDate,
    DateTime EndDate);

public record CalendarResponseDto(
    DateTime SelectedDate,
    IReadOnlyList<CalendarEventDto> DayEvents,
    IReadOnlyList<DateTime> MonthEventDates);
