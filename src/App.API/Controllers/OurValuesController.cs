using App.API.Controllers.Common;
using App.API.Extensions;
using App.BL.DTOs;
using App.BL.Services.Business.OurValues;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

/// <summary>
/// Dəyərlərimiz resurslarını idarə edir.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class OurValuesController(IOurValuesService ourValuesService) : ControllerBase
{
    /// <summary>
    /// Bütün dəyərlərin siyahısını qaytarır.
    /// </summary>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Dəyər DTO-larının siyahısı.</returns>
    /// <response code="200">Siyahı uğurla qaytarıldı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpGet]
    [ProducesResponseType(typeof(PagedDataResponse<IEnumerable<OurValuesResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var response = await ourValuesService.GetAllAsync(cancellationToken);
        return this.HandlePaginatedEnumerableResponse(response, pageIndex, pageSize);
    }

    /// <summary>
    /// Verilmiş ID-yə görə dəyəri qaytarır.
    /// </summary>
    /// <param name="id">Dəyərin unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Tapılan dəyər DTO-su.</returns>
    /// <response code="200">Dəyər uğurla tapıldı.</response>
    /// <response code="404">Dəyər tapılmadı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse<OurValuesResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await ourValuesService.GetByIdAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Yeni dəyər yaradır.
    /// </summary>
    /// <param name="dto">Yaradılacaq dəyərin məlumatları (multipart/form-data).</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Yaradılmış dəyər DTO-su.</returns>
    /// <response code="200">Dəyər uğurla yaradıldı.</response>
    /// <response code="422">Validasiya xətası.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpPost]
    [Authorize]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(SuccessResponse<OurValuesResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromForm] CreateOurValuesDto dto, CancellationToken cancellationToken = default)
    {
        var response = await ourValuesService.CreateAsync(dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Mövcud dəyəri yeniləyir.
    /// </summary>
    /// <param name="id">Yenilənəcək dəyərin unikal identifikatoru.</param>
    /// <param name="dto">Yenilənmiş dəyər məlumatları (multipart/form-data).</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Yenilənmiş dəyər DTO-su.</returns>
    /// <response code="200">Dəyər uğurla yeniləndi.</response>
    /// <response code="404">Dəyər tapılmadı.</response>
    /// <response code="422">Validasiya xətası.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpPut("{id:guid}")]
    [Authorize]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(SuccessResponse<OurValuesResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] UpdateOurValuesDto dto, CancellationToken cancellationToken = default)
    {
        var response = await ourValuesService.UpdateAsync(id, dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Dəyəri həmişəlik silir.
    /// </summary>
    /// <param name="id">Silinəcək dəyərin unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Əməliyyatın nəticəsi.</returns>
    /// <response code="200">Dəyər uğurla silindi.</response>
    /// <response code="404">Dəyər tapılmadı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpDelete("{id:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(SuccessResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await ourValuesService.DeleteAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }
}
