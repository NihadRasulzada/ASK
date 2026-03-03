using App.BL.DTOs;

namespace App.BL.Services.Abstractions;

/// <summary>
/// Valyuta məzənnəsi əməliyyatları üçün servis müqaviləsi.
/// </summary>
public interface ICurrencyService
{
    /// <summary>
    /// EUR, USD, TRY, RUB valyutalarının AZN-ə nisbətən cari məzənnəsini
    /// və 24 saatlıq dəyişiklik faizini yalnız DB-dən oxuyaraq qaytarır.
    /// Nəticə gün sonuna qədər yaddaşda saxlanılır.
    /// </summary>
    Task<IEnumerable<CurrencyRateDto>> GetRatesAsync();

    /// <summary>
    /// Xarici API-dan cari məzənnələri çəkib DB-yə upsert edir,
    /// 30 gündən köhnə yazıları DB-dən silir və cache-i sıfırlayır.
    /// Bu metodu yalnız cron job çağırır.
    /// </summary>
    Task FetchAndSaveRatesAsync();
}
