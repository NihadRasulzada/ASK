using System.Text.Json;
using App.BL.DTOs;
using App.BL.Settings;
using App.Core.Enums;
using App.Core.Interfaces.Repository.CurrencyRate;
using App.Core.Interfaces.Repository.UserPeriodSetting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace App.BL.Services.Business.CurrencyRate;

public class CurrencyService : ICurrencyService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ExchangeRateSettings _settings;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<CurrencyService> _logger;

    private static readonly string[] TargetCurrencies = ["EUR", "USD", "TRY", "RUB"];

    public CurrencyService(
        IHttpClientFactory httpClientFactory,
        IOptions<ExchangeRateSettings> settings,
        IServiceScopeFactory scopeFactory,
        ILogger<CurrencyService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _settings = settings.Value;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    private static string GetPeriodLabel(RatePeriod period) => period switch
    {
        RatePeriod.Day => "Son 1 günün hesabatı",
        RatePeriod.Week => "Son 1 həftənin hesabatı",
        RatePeriod.Month => "Son 1 ayın hesabatı",
        _ => "Naməlum müddət"
    };

    public async Task<PeriodSelectionDto> SetSelectedPeriodAsync(RatePeriod period)
    {
        using var scope = _scopeFactory.CreateScope();
        var readRepo = scope.ServiceProvider.GetRequiredService<IUserPeriodSettingReadRepository>();
        var writeRepo = scope.ServiceProvider.GetRequiredService<IUserPeriodSettingWriteRepository>();

        var existing = await readRepo.GetCurrentAsync();

        if (existing is not null)
        {
            existing.UpdatePeriod(period);
            writeRepo.Update(existing);
        }
        else
        {
            var newSetting = new Core.Entities.UserPeriodSetting(period);
            await writeRepo.AddAsync(newSetting, CancellationToken.None);
        }

        await writeRepo.SaveChangesAsync(CancellationToken.None);

        return new PeriodSelectionDto(period, GetPeriodLabel(period));
    }

    public async Task<CurrencyRatesResponseDto> GetRatesAsync(CancellationToken cancellationToken = default)
    {
        using var scope = _scopeFactory.CreateScope();
        var readRepo = scope.ServiceProvider.GetRequiredService<ICurrencyRateReadRepository>();
        var settingRepo = scope.ServiceProvider.GetRequiredService<IUserPeriodSettingReadRepository>();

        var setting = await settingRepo.GetCurrentAsync(cancellationToken);
        var period = setting?.SelectedPeriod ?? RatePeriod.Day;

        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var pastDate = today.AddDays(-(int)period);

        var allRates = await readRepo.GetAllAsync(false, cancellationToken);
        var targetRates = allRates
            .Where(r => TargetCurrencies.Contains(r.CurrencyCode))
            .ToList();

        var latestRates = targetRates
            .GroupBy(r => r.CurrencyCode)
            .Select(g => g.OrderByDescending(r => r.RateDate).First())
            .ToList();

        var result = new List<CurrencyRateDto>();

        foreach (var current in latestRates)
        {
            var pastRate = targetRates
                .Where(r => r.CurrencyCode == current.CurrencyCode && r.RateDate <= pastDate)
                .OrderByDescending(r => r.RateDate)
                .FirstOrDefault();

            decimal changePercent = 0;
            if (pastRate is not null && pastRate.Rate != 0)
                changePercent = Math.Round(
                    ((current.Rate - pastRate.Rate) / pastRate.Rate) * 100, 2);

            result.Add(new CurrencyRateDto(current.CurrencyCode, current.Rate, changePercent));
        }

        var ordered = result.OrderBy(r => Array.IndexOf(TargetCurrencies, r.Code));

        return new CurrencyRatesResponseDto(GetPeriodLabel(period), period, ordered);
    }

    public async Task FetchAndSaveRatesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            using var scope = _scopeFactory.CreateScope();
            var readRepo = scope.ServiceProvider.GetRequiredService<ICurrencyRateReadRepository>();
            var writeRepo = scope.ServiceProvider.GetRequiredService<ICurrencyRateWriteRepository>();

            var client = _httpClientFactory.CreateClient("ExchangeRate");
            var url = $"https://v6.exchangerate-api.com/v6/{_settings.ApiKey}/latest/AZN";
            var response = await client.GetAsync(url, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogWarning("API FAILED. Status: {StatusCode}, Body: {Body}", response.StatusCode, errorBody);
                return;
            }

            var json = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogInformation("API RESPONSE: {Json}", json);
            using var doc = JsonDocument.Parse(json);

            if (!doc.RootElement.TryGetProperty("conversion_rates", out var ratesElement))
            {
                _logger.LogWarning("conversion_rates not found!");
                return;
            }

            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            foreach (var currency in TargetCurrencies)
            {
                if (!ratesElement.TryGetProperty(currency, out var rateElement))
                    continue;

                var rate = rateElement.GetDecimal();
                var existing = await readRepo.GetAsync(
                    r => r.CurrencyCode == currency && r.RateDate == today,
                    true,
                    cancellationToken);

                if (existing is not null)
                {
                    existing.UpdateRate(rate);
                    writeRepo.Update(existing);
                }
                else
                {
                    var newRate = new Core.Entities.CurrencyRate(currency, rate, today);
                    await writeRepo.AddAsync(newRate, cancellationToken);
                }
            }

            _logger.LogInformation("SAVING TO DB...");
            await writeRepo.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("SAVE DONE");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch and save currency rates.");
        }
    }
}
