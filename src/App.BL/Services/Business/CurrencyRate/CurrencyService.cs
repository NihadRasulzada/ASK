using App.BL.DTOs;
using App.BL.Settings;
using App.Core.Interfaces.Repository.CurrencyRate;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

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

    public async Task<IEnumerable<CurrencyRateDto>> GetRatesAsync(CancellationToken cancellationToken = default)
    {
        using var scope = _scopeFactory.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<ICurrencyRateReadRepository>();

        var rates = await repo.GetAllAsync(false, cancellationToken);

        return rates.Select(r => new CurrencyRateDto(r.CurrencyCode, r.Rate, 0));
    }

    public async Task FetchAndSaveRatesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            using var scope = _scopeFactory.CreateScope();
            var readRepo = scope.ServiceProvider.GetRequiredService<ICurrencyRateReadRepository>();
            var writeRepo = scope.ServiceProvider.GetRequiredService<ICurrencyRateWriteRepository>();

            var client = _httpClientFactory.CreateClient("ExchangeRate");
            //var response = await client.GetAsync($"latest?access_key={_settings.ApiKey}&symbols={string.Join(",", TargetCurrencies)}&base=AZN", cancellationToken);
            //var response = await client.GetAsync($"{_settings.ApiKey}/latest/AZN",cancellationToken);
            var url = $"https://v6.exchangerate-api.com/v6/{_settings.ApiKey}/latest/AZN";
            var response = await client.GetAsync(url, cancellationToken);


            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogWarning("API FAILED. Status: {StatusCode}, Body: {Body}", response.StatusCode, errorBody);
                return;
            }
            


            var json = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogInformation("API RESPONSE: {Json}", json); // sonradan men eleve etdim bunu
            using var doc = JsonDocument.Parse(json);

            if (!doc.RootElement.TryGetProperty("conversion_rates", out var ratesElement))
            {
                _logger.LogWarning("conversion_rates not found!");
                return;
            }



            //if (!doc.RootElement.TryGetProperty("rates", out var ratesElement))
            //    return;
            

            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            _logger.LogInformation("ENTERING SAVE LOOP");

            foreach (var currency in TargetCurrencies)
            {
                if (!ratesElement.TryGetProperty(currency, out var rateElement))
                    continue;

                var rate = rateElement.GetDecimal();
                var existing = await readRepo.GetAsync(r => r.CurrencyCode == currency && r.RateDate == today, true, cancellationToken);

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

            //await writeRepo.SaveChangesAsync(cancellationToken);

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
