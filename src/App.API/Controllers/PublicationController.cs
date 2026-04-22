using App.API.Controllers.Common;
using App.API.Extensions;
using App.BL.DTOs;
using App.BL.Services.Business.Publication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

/// <summary>
/// Nəşrlər resurslarını idarə edir.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PublicationController(IPublicationService publicationService) : ControllerBase
{
    /// <summary>
    /// Nəşrlərin səhifələnmiş siyahısını qaytarır.
    /// </summary>
    /// <param name="pageIndex">Səhifə nömrəsi (1-dən başlayır).</param>
    /// <param name="pageSize">Hər səhifədəki element sayı.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Nəşr DTO-larının səhifələnmiş siyahısı.</returns>
    /// <response code="200">Siyahı uğurla qaytarıldı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpGet]
    [ProducesResponseType(typeof(PagedDataResponse<PublicationResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var response = await publicationService.GetAllAsync(pageIndex, pageSize, cancellationToken);
        return this.HandlePagedServiceResponse(response);
    }

    /// <summary>
    /// Verilmiş ID-yə görə nəşr məlumatlarını qaytarır.
    /// </summary>
    /// <param name="id">Nəşrin unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Tapılan nəşr DTO-su.</returns>
    /// <response code="200">Nəşr uğurla tapıldı.</response>
    /// <response code="404">Nəşr tapılmadı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse<PublicationResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await publicationService.GetByIdAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Yeni nəşr yaradır.
    /// </summary>
    /// <param name="dto">Yaradılacaq nəşrin məlumatları (multipart/form-data).</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Yaradılmış nəşr DTO-su.</returns>
    /// <response code="200">Nəşr uğurla yaradıldı.</response>
    /// <response code="422">Validasiya xətası.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpPost]
    [Authorize]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(SuccessResponse<PublicationResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromForm] CreatePublicationDto dto, CancellationToken cancellationToken = default)
    {
        var response = await publicationService.CreateAsync(dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Mövcud nəşri yeniləyir.
    /// </summary>
    /// <param name="id">Yenilənəcək nəşrin unikal identifikatoru.</param>
    /// <param name="dto">Yenilənmiş nəşr məlumatları (multipart/form-data).</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Yenilənmiş nəşr DTO-su.</returns>
    /// <response code="200">Nəşr uğurla yeniləndi.</response>
    /// <response code="404">Nəşr tapılmadı.</response>
    /// <response code="422">Validasiya xətası.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpPut("{id:guid}")]
    [Authorize]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(SuccessResponse<PublicationResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] UpdatePublicationDto dto, CancellationToken cancellationToken = default)
    {
        var response = await publicationService.UpdateAsync(id, dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Nəşri həmişəlik silir.
    /// </summary>
    /// <param name="id">Silinəcək nəşrin unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Əməliyyatın nəticəsi.</returns>
    /// <response code="200">Nəşr uğurla silindi.</response>
    /// <response code="404">Nəşr tapılmadı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpDelete("{id:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await publicationService.DeleteAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }
}
