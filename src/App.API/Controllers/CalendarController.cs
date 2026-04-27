using App.API.Controllers.Common;
using App.API.Extensions;
using App.BL.DTOs;
using App.BL.Services.Business.Calendar;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

/// <summary>
/// Təqvim resurslarını idarə edir.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CalendarController(ICalendarService calendarService) : ControllerBase
{
    /// <summary>
    /// Seçilən tarixə aid tədbirləri və həmin ayın tədbir tarixlərini qaytarır.
    /// </summary>
    /// <param name="date">Seçilən tarix (format: yyyy-MM-dd).</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Günün tədbirləri + ayın highlight tarixləri.</returns>
    /// <response code="200">Məlumatlar uğurla qaytarıldı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpGet]
    [ProducesResponseType(typeof(SuccessResponse<CalendarResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByDate(
        [FromQuery] DateTime date,
        CancellationToken cancellationToken = default)
    {
        var response = await calendarService.GetByDateAsync(date, cancellationToken);
        return this.HandleServiceResponse(response);
    }
}

