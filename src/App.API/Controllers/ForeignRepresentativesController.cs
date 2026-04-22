using App.API.Controllers.Common;
using App.API.Extensions;
using App.BL.DTOs;
using App.BL.Services.Business.ForeignRepresentatives;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

/// <summary>
/// Xarici nümayəndəliklər resurslarını idarə edir.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ForeignRepresentativesController(IForeignRepresentativesService foreignRepresentativesService) : ControllerBase
{
    /// <summary>
    /// Bütün xarici nümayəndəliklərin siyahısını qaytarır.
    /// </summary>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Xarici nümayəndəlik DTO-larının siyahısı.</returns>
    /// <response code="200">Siyahı uğurla qaytarıldı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpGet]
    [ProducesResponseType(typeof(SuccessResponse<IEnumerable<ForeignRepresentativesResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var response = await foreignRepresentativesService.GetAllAsync(cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Verilmiş ID-yə görə xarici nümayəndəliyi qaytarır.
    /// </summary>
    /// <param name="id">Nümayəndəliyin unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Tapılan xarici nümayəndəlik DTO-su.</returns>
    /// <response code="200">Nümayəndəlik uğurla tapıldı.</response>
    /// <response code="404">Nümayəndəlik tapılmadı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse<ForeignRepresentativesResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await foreignRepresentativesService.GetByIdAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Yeni xarici nümayəndəlik qeydi yaradır.
    /// </summary>
    /// <param name="dto">Yaradılacaq nümayəndəliyin məlumatları.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Yaradılmış xarici nümayəndəlik DTO-su.</returns>
    /// <response code="200">Nümayəndəlik uğurla yaradıldı.</response>
    /// <response code="422">Validasiya xətası.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(SuccessResponse<ForeignRepresentativesResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateForeignRepresentativesDto dto, CancellationToken cancellationToken = default)
    {
        var response = await foreignRepresentativesService.CreateAsync(dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Mövcud xarici nümayəndəliyi yeniləyir.
    /// </summary>
    /// <param name="id">Yenilənəcək nümayəndəliyin unikal identifikatoru.</param>
    /// <param name="dto">Yenilənmiş nümayəndəlik məlumatları.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Yenilənmiş xarici nümayəndəlik DTO-su.</returns>
    /// <response code="200">Nümayəndəlik uğurla yeniləndi.</response>
    /// <response code="404">Nümayəndəlik tapılmadı.</response>
    /// <response code="422">Validasiya xətası.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpPut("{id:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(SuccessResponse<ForeignRepresentativesResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateForeignRepresentativesDto dto, CancellationToken cancellationToken = default)
    {
        var response = await foreignRepresentativesService.UpdateAsync(id, dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Xarici nümayəndəliyi həmişəlik silir.
    /// </summary>
    /// <param name="id">Silinəcək nümayəndəliyin unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Əməliyyatın nəticəsi.</returns>
    /// <response code="200">Nümayəndəlik uğurla silindi.</response>
    /// <response code="404">Nümayəndəlik tapılmadı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpDelete("{id:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(SuccessResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await foreignRepresentativesService.DeleteAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }
}
