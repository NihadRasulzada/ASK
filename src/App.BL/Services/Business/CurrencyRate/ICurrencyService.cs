using App.BL.DTOs;
using App.Core.Enums;

namespace App.BL.Services.Business.CurrencyRate;

public interface ICurrencyService
{
    Task<PeriodSelectionDto> SetSelectedPeriodAsync(RatePeriod period);
    Task<CurrencyRatesResponseDto> GetRatesAsync(CancellationToken cancellationToken = default);
    Task FetchAndSaveRatesAsync(CancellationToken cancellationToken = default);
}
