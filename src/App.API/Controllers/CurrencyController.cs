using App.BL.DTOs;
using App.BL.Services.Business.CurrencyRate;
using App.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

/// <summary>
/// Valyuta məzənnəsi resurslarını idarə edir.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CurrencyController(ICurrencyService currencyService) : ControllerBase
{
    /// <summary>
    /// Seçilmiş müddətə görə valyuta məzənnəsi və dəyişiklik faizini qaytarır.
    /// DB-də seçimi saxlayır. Sonrakı /api/currency sorğusu bu seçimə görə cavab verəcək.
    /// </summary>
    /// <param name="period">Day = 1 gün, Week = 1 həftə, Month = 1 ay</param>
    /// <response code="200">Seçim uğurla saxlanıldı.</response>
    /// <response code="400">Yanlış müddət dəyəri.</response>
    [HttpGet("change")]
    [Authorize]
    [ProducesResponseType(typeof(PeriodSelectionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SetPeriod([FromQuery] RatePeriod period = RatePeriod.Day)
    {
        if (!Enum.IsDefined(typeof(RatePeriod), period))
            return BadRequest("Yanlış müddət. Day, Week, Month seçin.");

        var result = await currencyService.SetSelectedPeriodAsync(period);
        return Ok(result);
    }

    /// <summary>
    /// /change endpointindəki seçimə görə valyuta məzənnələrini qaytarır.
    /// ChangePercent seçilmiş müddətə görə hesablanır.
    /// </summary>
    /// <response code="200">Valyuta məzənnələri uğurla qaytarıldı.</response>
    /// <response code="500">Xəta baş verdi.</response>
    [HttpGet]
    [ProducesResponseType(typeof(CurrencyRatesResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRates(CancellationToken cancellationToken = default)
    {
        var result = await currencyService.GetRatesAsync(cancellationToken);
        return Ok(result);
    }
}
