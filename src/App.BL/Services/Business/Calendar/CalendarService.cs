using App.BL.DTOs;
using App.BL.Services.External;
using App.Core.Entities;
using App.Core.Interfaces.Repository.BusinessForum;
using App.Core.Interfaces.Repository.Common;
using App.Core.Interfaces.Repository.Exhibition;
using App.Core.Interfaces.Repository.Training;
using App.Core.ResponseObject.Concreate;
using ExhibitionEntity = App.Core.Entities.Exhibition;
using TrainingEntity = App.Core.Entities.Training;
using BusinessForumEntity = App.Core.Entities.BusinessForum;

namespace App.BL.Services.Business.Calendar;

public class CalendarService(
    IExhibitionReadRepository exhibitionReadRepository,
    ITrainingReadRepository trainingReadRepository,
    IBusinessForumReadRepository businessForumReadRepository,
    IMediaUrlBuilder mediaUrlBuilder) : ICalendarService
{
    public async Task<Response<CalendarResponseDto>> GetByDateAsync(
        DateTime date,
        CancellationToken cancellationToken = default)
    {
        var targetDate = date.Date;
        var firstDay = new DateTime(targetDate.Year, targetDate.Month, 1);
        var lastDay = firstDay.AddMonths(1).AddDays(-1);

        // Ay üçün bütün event-ləri bir dəfə çəkirik
        var exhibitions = await exhibitionReadRepository.GetAllAsync(
            enableTracking: false,
            cancellationToken: cancellationToken,
            ignoreQueryFilters: false,
            predicate: e => e.StartDate.Date <= lastDay && e.EndDate.Date >= firstDay);

        var trainings = await trainingReadRepository.GetAllAsync(
            enableTracking: false,
            cancellationToken: cancellationToken,
            ignoreQueryFilters: false,
            predicate: t => t.StartDate.Date <= lastDay && t.EndDate.Date >= firstDay);

        var biznessform = await businessForumReadRepository.GetAllAsync(
            enableTracking: false,
            cancellationToken: cancellationToken,
            ignoreQueryFilters: false,
            predicate: b => b.StartDate.Date <= lastDay && b.EndDate.Date >= firstDay);

        // Seçilən günün event-ləri
        var dayEvents = exhibitions
            .Where(e => e.StartDate.Date <= targetDate && e.EndDate.Date >= targetDate)
            .Select(e => ToDto(e, "Exhibition"))
            .Concat(trainings
                .Where(t => t.StartDate.Date <= targetDate && t.EndDate.Date >= targetDate)
                .Select(t => ToDto(t, "Training")))
            .Concat(biznessform
                .Where(b => b.StartDate.Date <= targetDate && b.EndDate.Date >= targetDate)
                .Select(b => ToDto(b, "BusinessForum")))
            .OrderBy(x => x.StartDate)
            .ToList();

        // Ayın highlight tarixləri
        var eventDates = new HashSet<DateTime>();

        foreach (var e in exhibitions)
            ExpandDatesIntoMonth(e.StartDate, e.EndDate, firstDay, lastDay, eventDates);

        foreach (var t in trainings)
            ExpandDatesIntoMonth(t.StartDate, t.EndDate, firstDay, lastDay, eventDates);

        foreach (var t in biznessform)
            ExpandDatesIntoMonth(t.StartDate, t.EndDate, firstDay, lastDay, eventDates);

        var result = new CalendarResponseDto(
            targetDate,
            dayEvents,
            eventDates.OrderBy(d => d).ToList());

        return Response<CalendarResponseDto>.Success(result, "Tədbirlər uğurla qaytarıldı.");
    }

    private CalendarEventDto ToDto(ExhibitionEntity e, string eventType) =>
        new(e.Id, eventType, e.TitleAz, e.TitleEn, e.TitleRu,
            mediaUrlBuilder.Build(e.TitleImageUrl.ObjectKey), e.StartDate, e.EndDate);

    private CalendarEventDto ToDto(TrainingEntity t, string eventType) =>
        new(t.Id, eventType, t.TitleAz, t.TitleEn, t.TitleRu,
            mediaUrlBuilder.Build(t.TitleImageUrl.ObjectKey), t.StartDate, t.EndDate);

    private CalendarEventDto ToDto(BusinessForumEntity b, string eventType) =>
        new(b.Id, eventType, b.TitleAz, b.TitleEn, b.TitleRu,
            mediaUrlBuilder.Build(b.TitleImageUrl.ObjectKey), b.StartDate, b.EndDate);

    private static void ExpandDatesIntoMonth(
        DateTime startDate,
        DateTime endDate,
        DateTime firstDay,
        DateTime lastDay,
        HashSet<DateTime> eventDates)
    {
        var from = startDate.Date < firstDay ? firstDay : startDate.Date;
        var to = endDate.Date > lastDay ? lastDay : endDate.Date;

        for (var d = from; d <= to; d = d.AddDays(1))
            eventDates.Add(d);
    }
}
