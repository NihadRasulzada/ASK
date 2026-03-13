using App.API.Controllers.Common;
using App.API.Extensions;
using App.BL.DTOs;
using App.BL.Services.Business.President;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

/// <summary>
/// Prezident məlumatlarını idarə edir.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PresidentController(IPresidentService presidentService) : ControllerBase
{
    /// <summary>
    /// Prezident məlumatlarını qaytarır.
    /// </summary>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Prezident DTO-larının siyahısı.</returns>
    /// <response code="200">Məlumatlar uğurla qaytarıldı.</response>
    [HttpGet]
    [ProducesResponseType(typeof(SuccessResponse<IEnumerable<PresidentResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var response = await presidentService.GetAllAsync(cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Verilmiş ID-yə görə prezident məlumatını qaytarır.
    /// </summary>
    /// <param name="id">Məlumatın unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Tapılan prezident DTO-su.</returns>
    /// <response code="200">Məlumat uğurla tapıldı.</response>
    /// <response code="404">Məlumat tapılmadı.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse<PresidentResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await presidentService.GetByIdAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Yeni prezident məlumatı yaradır.
    /// </summary>
    /// <param name="dto">Yaradılacaq məlumatlar.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Əməliyyatın nəticəsi.</returns>
    /// <response code="200">Məlumat uğurla yaradıldı.</response>
    /// <response code="422">Validasiya xətası.</response>
    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create([FromForm] CreatePresidentDto dto, CancellationToken cancellationToken = default)
    {
        var response = await presidentService.CreateAsync(dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Mövcud prezident məlumatını yeniləyir.
    /// </summary>
    /// <param name="id">Yenilənəcək məlumatın unikal identifikatoru.</param>
    /// <param name="dto">Yenilənmiş məlumatlar.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Yenilənmiş DTO.</returns>
    /// <response code="200">Məlumat uğurla yeniləndi.</response>
    /// <response code="404">Məlumat tapılmadı.</response>
    /// <response code="422">Validasiya xətası.</response>
    [HttpPut("{id:guid}")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(SuccessResponse<PresidentResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] UpdatePresidentDto dto, CancellationToken cancellationToken = default)
    {
        var response = await presidentService.UpdateAsync(id, dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Prezident məlumatını həmişəlik silir.
    /// </summary>
    /// <param name="id">Silinəcək məlumatın unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Əməliyyatın nəticəsi.</returns>
    /// <response code="200">Məlumat uğurla silindi.</response>
    /// <response code="404">Məlumat tapılmadı.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await presidentService.DeleteAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }
}
