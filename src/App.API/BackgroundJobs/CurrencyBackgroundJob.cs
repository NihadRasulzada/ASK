using App.BL.Services.Business.CurrencyRate;

namespace App.API.BackgroundJobs;

/// <summary>
/// Hər gün saat 01:00-da valyuta məzənnələrini API-dan çəkib DB-yə saxlayan background servis.
/// Proqram başlayanda da dərhal bir dəfə icra olunur.
/// </summary>
//public class CurrencyBackgroundJob : BackgroundService
//{
//    private readonly ICurrencyService _currencyService;
//    private readonly ILogger<CurrencyBackgroundJob> _logger;

//    public CurrencyBackgroundJob(
//        ICurrencyService currencyService,
//        ILogger<CurrencyBackgroundJob> logger)
//    {
//        _currencyService = currencyService;
//        _logger = logger;
//    }

    //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    //{
    //    // Proqram başlayanda DB-ni dərhal doldur
    //    await RunJobAsync();

    //    while (!stoppingToken.IsCancellationRequested)
    //    {
    //        // Növbəti 01:00-a qədər gözlə (server local time)
    //        var now = DateTime.Now;
    //        var next1AM = DateTime.Today
    //            .AddDays(now.Hour >= 1 ? 1 : 0)
    //            .AddHours(1);

    //        var delay = next1AM - now;

    //        _logger.LogInformation(
    //            "Növbəti valyuta məzənnəsi yeniləməsi {Next} tarixdə (delay: {Delay:hh\\:mm\\:ss}).",
    //            next1AM, delay);

    //        try
    //        {
    //            await Task.Delay(delay, stoppingToken);
    //        }
    //        catch (OperationCanceledException)
    //        {
    //            break;
    //        }

    //        await RunJobAsync();
    //    }
    //}

    //private async Task RunJobAsync()
    //{
    //    try
    //    {
    //        _logger.LogInformation("Valyuta məzənnəsi yeniləməsi başladı.");
    //        await _currencyService.FetchAndSaveRatesAsync();
    //        _logger.LogInformation("Valyuta məzənnəsi uğurla yeniləndi.");
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Valyuta məzənnəsi yenilənərkən xəta baş verdi.");
    //    }
    //}
//}
