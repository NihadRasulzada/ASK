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
public class CurrencyController : ControllerBase
{
    //private readonly ICurrencyService _currencyService;

    ///// <summary>
    ///// CurrencyController-i başladır.
    ///// </summary>
    ///// <param name="currencyService">Valyuta servis instansiyası.</param>
    //public CurrencyController(ICurrencyService currencyService)
    //{
    //    _currencyService = currencyService;
    //}

    ///// <summary>
    ///// EUR, USD, TRY, RUB valyutalarının AZN-ə nisbətən cari məzənnəsini
    ///// və 24 saatlıq dəyişiklik faizini qaytarır.
    ///// Nəticə gecə yarısına qədər yaddaşda saxlanılır.
    ///// </summary>
    ///// <returns>Valyuta məzənnələrinin siyahısı.</returns>
    ///// <response code="200">Valyuta məzənnələri uğurla qaytarıldı.</response>
    ///// <response code="500">Xarici API sorğusu zamanı xəta baş verdi.</response>
    //[HttpGet]
    //[ProducesResponseType(typeof(IEnumerable<CurrencyRateDto>), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> GetRates()
    //{
    //    var rates = await _currencyService.GetRatesAsync();
    //    return Ok(rates);
    //}
}
