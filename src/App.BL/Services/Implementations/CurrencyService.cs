using App.BL.DTOs;
using App.BL.Services.Abstractions;
using App.BL.Settings;
using App.Core.Entities;
using App.Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace App.BL.Services.Implementations;

public class CurrencyService : ICurrencyService
{
    private static readonly string[] _currencies = ["EUR", "USD", "TRY", "RUB"];
    private const string _cacheKey = "currency_rates";

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMemoryCache _cache;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ExchangeRateSettings _settings;

    public CurrencyService(
        IHttpClientFactory httpClientFactory,
        IMemoryCache cache,
        IServiceScopeFactory scopeFactory,
        IOptions<ExchangeRateSettings> settings)
    {
        _httpClientFactory = httpClientFactory;
        _cache = cache;
        _scopeFactory = scopeFactory;
        _settings = settings.Value;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<CurrencyRateDto>> GetRatesAsync()
    {
        // Cache HIT — heç bir sorğu atmadan dərhal cavab ver
        if (_cache.TryGetValue(_cacheKey, out IEnumerable<CurrencyRateDto>? cached) && cached is not null)
            return cached;

        // Cache MISS — yalnız DB-dən oxu (API-a sorğu yoxdur)
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        using var scope = _scopeFactory.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<ICurrencyRateRepository>();

        var todayRates = (await repo.GetByDateAsync(today))
            .ToDictionary(r => r.CurrencyCode, r => r.Rate);

        var yesterdayRates = (await repo.GetByDateAsync(today.AddDays(-1)))
            .ToDictionary(r => r.CurrencyCode, r => r.Rate);

        var results = _currencies.Select(code =>
        {
            todayRates.TryGetValue(code, out var currentRate);
            yesterdayRates.TryGetValue(code, out var yesterdayRate);

            var changePercent = yesterdayRate == 0m
                ? 0m
                : Math.Round(((currentRate - yesterdayRate) / yesterdayRate) * 100m, 2);

            return new CurrencyRateDto(code, Math.Round(currentRate, 4), changePercent);
        }).ToList();

        var expiry = DateTimeOffset.UtcNow.Date.AddDays(1);
        _cache.Set(_cacheKey, (IEnumerable<CurrencyRateDto>)results, new MemoryCacheEntryOptions
        {
            AbsoluteExpiration = expiry
        });

        return results;
    }

    /// <inheritdoc/>
    public async Task FetchAndSaveRatesAsync()
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var client = _httpClientFactory.CreateClient("ExchangeRate");

        // 4 paralel API sorğusu
        var tasks = _currencies.Select(code => FetchCurrentRateAsync(client, code)).ToArray();
        await Task.WhenAll(tasks);

        using var scope = _scopeFactory.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<ICurrencyRateRepository>();

        // Bu günün məzənnəsini DB-yə upsert et
        for (int i = 0; i < _currencies.Length; i++)
        {
            var code = _currencies[i];
            var rate = await tasks[i];

            var existing = await repo.GetByCodeAndDateAsync(code, today);
            if (existing is not null)
                existing.UpdateRate(rate);
            else
                await repo.AddAsync(new CurrencyRate(code, rate, today));
        }

        // 30 gündən köhnə yazıları sil
        var cutoff = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-30));
        await repo.DeleteOlderThanAsync(cutoff);

        await repo.SaveChangesAsync();

        // Cache-i sıfırla ki növbəti endpoint sorğusu yeni datanı DB-dən oxusun
        _cache.Remove(_cacheKey);
    }

    private async Task<decimal> FetchCurrentRateAsync(HttpClient client, string currencyCode)
    {
        var response = await client.GetAsync($"{_settings.ApiKey}/pair/{currencyCode}/AZN");
        response.EnsureSuccessStatusCode();

        var data = await response.Content.ReadFromJsonAsync<CurrentRateResponse>()
            ?? throw new InvalidOperationException($"Cari məzənnə cavabı boş gəldi: {currencyCode}");

        return data.ConversionRate;
    }

    private sealed record CurrentRateResponse(
        [property: JsonPropertyName("conversion_rate")]
        decimal ConversionRate);
}
