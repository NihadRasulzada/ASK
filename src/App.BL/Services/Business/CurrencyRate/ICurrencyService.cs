using App.BL.DTOs;

namespace App.BL.Services.Business.CurrencyRate;

public interface ICurrencyService
{
    Task<IEnumerable<CurrencyRateDto>> GetRatesAsync(CancellationToken cancellationToken = default);
    Task FetchAndSaveRatesAsync(CancellationToken cancellationToken = default);
}
