using App.API.Controllers.Common;
using App.API.Extensions;
using App.BL.DTOs;
using App.BL.Services.Business.Setting;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

/// <summary>
/// Sistem parametrlərini idarə edir.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class SettingController(ISettingService settingService) : ControllerBase
{
    /// <summary>
    /// Bütün sistem parametrlərini qaytarır.
    /// </summary>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Parametrlərin siyahısı.</returns>
    /// <response code="200">Siyahı uğurla qaytarıldı.</response>
    [HttpGet]
    [ProducesResponseType(typeof(SuccessResponse<IEnumerable<SettingResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var response = await settingService.GetAllAsync(cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Açarına görə tək parametr qaytarır.
    /// </summary>
    /// <param name="key">Setting açarı (məs. BasKollektivSazis).</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Parametr məlumatı.</returns>
    /// <response code="200">Parametr uğurla tapıldı.</response>
    /// <response code="404">Parametr tapılmadı.</response>
    [HttpGet("{key}")]
    [ProducesResponseType(typeof(SuccessResponse<SettingResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByKey(
        [FromRoute] string key,
        CancellationToken cancellationToken = default)
    {
        var response = await settingService.GetByKeyAsync(key, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Mövcud parametrin dəyərini yeniləyir.
    /// Link tipli parametr üçün PdfFile (PDF fayl), Text tipli üçün Value (mətn) göndərilməlidir.
    /// </summary>
    /// <param name="key">Setting açarı.</param>
    /// <param name="dto">Yenilənəcək dəyər.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Yenilənmiş parametr məlumatı.</returns>
    /// <response code="200">Parametr uğurla yeniləndi.</response>
    /// <response code="400">Yanlış dəyər (tip uyğunsuzluğu).</response>
    /// <response code="404">Parametr tapılmadı.</response>
    [HttpPut("{key}")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(SuccessResponse<SettingResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(
        [FromRoute] string key,
        [FromForm] UpdateSettingDto dto,
        CancellationToken cancellationToken = default)
    {
        var response = await settingService.UpdateAsync(key, dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }
}
