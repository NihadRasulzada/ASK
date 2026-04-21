using App.API.Controllers.Common;
using App.API.Extensions;
using App.BL.DTOs;
using App.BL.Services.Business.Partner;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

/// <summary>
/// Partnyor resurslarını idarə edir.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PartnerController(IPartnerService partnerService) : ControllerBase
{
    /// <summary>
    /// Bütün partnyorların siyahısını qaytarır.
    /// </summary>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Partnyor DTO-larının siyahısı.</returns>
    /// <response code="200">Siyahı uğurla qaytarıldı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpGet]
    [ProducesResponseType(typeof(SuccessResponse<IEnumerable<PartnerResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var response = await partnerService.GetAllAsync(cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Verilmiş ID-yə görə partnyoru qaytarır.
    /// </summary>
    /// <param name="id">Partnyorun unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Tapılan partnyor DTO-su.</returns>
    /// <response code="200">Partnyor uğurla tapıldı.</response>
    /// <response code="404">Partnyor tapılmadı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse<PartnerResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await partnerService.GetByIdAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Yeni partnyor yaradır.
    /// </summary>
    /// <param name="dto">Yaradılacaq partnyorun məlumatları (multipart/form-data).</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Əməliyyatın nəticəsi.</returns>
    /// <response code="200">Partnyor uğurla yaradıldı.</response>
    /// <response code="422">Validasiya xətası.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpPost]
    [Authorize]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromForm] CreatePartnerDto dto, CancellationToken cancellationToken = default)
    {
        var response = await partnerService.CreateAsync(dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Mövcud partnyoru yeniləyir.
    /// </summary>
    /// <param name="id">Yenilənəcək partnyorun unikal identifikatoru.</param>
    /// <param name="dto">Yenilənmiş partnyor məlumatları (multipart/form-data).</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Yenilənmiş partnyor DTO-su.</returns>
    /// <response code="200">Partnyor uğurla yeniləndi.</response>
    /// <response code="404">Partnyor tapılmadı.</response>
    /// <response code="422">Validasiya xətası.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpPut("{id:guid}")]
    [Authorize]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(SuccessResponse<PartnerResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] UpdatePartnerDto dto, CancellationToken cancellationToken = default)
    {
        var response = await partnerService.UpdateAsync(id, dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Partnyoru həmişəlik silir.
    /// </summary>
    /// <param name="id">Silinəcək partnyorun unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Əməliyyatın nəticəsi.</returns>
    /// <response code="200">Partnyor uğurla silindi.</response>
    /// <response code="404">Partnyor tapılmadı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpDelete("{id:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await partnerService.DeleteAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }
}
