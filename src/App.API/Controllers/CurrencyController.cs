using App.BL.DTOs;
using App.BL.Services.Business.CurrencyRate;
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
    /// EUR, USD, TRY, RUB valyutalarının AZN-ə nisbətən cari məzənnəsini qaytarır.
    /// Nəticə hər gün gecə saat 01:00-da yenilənir.
    /// </summary>
    /// <returns>Valyuta məzənnələrinin siyahısı.</returns>
    /// <response code="200">Valyuta məzənnələri uğurla qaytarıldı.</response>
    /// <response code="500">Xəta baş verdi.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CurrencyRateDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRates(CancellationToken cancellationToken = default)
    {
        var rates = await currencyService.GetRatesAsync(cancellationToken);
        return Ok(rates);
    }
}
