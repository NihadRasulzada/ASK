using App.API.Controllers.Common;
using App.API.Extensions;
using App.BL.DTOs;
using App.BL.Services.Business.Director;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

/// <summary>
/// Rəhbərlik (Direktorlar) resurslarını idarə edir.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class DirectorController(IDirectorService directorService) : ControllerBase
{
    /// <summary>
    /// Aktiv rəhbərlərin siyahısını qaytarır.
    /// </summary>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Rəhbər DTO-larının siyahısı.</returns>
    /// <response code="200">Siyahı uğurla qaytarıldı.</response>
    [HttpGet]
    [ProducesResponseType(typeof(SuccessResponse<IEnumerable<DirectorResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var response = await directorService.GetAllAsync(cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Bütün rəhbərlərin (silinmişlər daxil olmaqla) siyahısını qaytarır.
    /// </summary>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Rəhbər DTO-larının siyahısı.</returns>
    /// <response code="200">Siyahı uğurla qaytarıldı.</response>
    [HttpGet("all")]
    [ProducesResponseType(typeof(SuccessResponse<IEnumerable<DirectorResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllIncludingDeleted(CancellationToken cancellationToken = default)
    {
        var response = await directorService.GetAllIncludingDeletedAsync(cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Verilmiş ID-yə görə rəhbər məlumatlarını qaytarır.
    /// </summary>
    /// <param name="id">Rəhbərin unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Tapılan rəhbər DTO-su.</returns>
    /// <response code="200">Rəhbər uğurla tapıldı.</response>
    /// <response code="404">Rəhbər tapılmadı.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse<DirectorResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await directorService.GetByIdAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Yeni rəhbər yaradır.
    /// </summary>
    /// <param name="dto">Yaradılacaq rəhbərin məlumatları.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Yaradılmış rəhbər məlumatı.</returns>
    /// <response code="200">Rəhbər uğurla yaradıldı.</response>
    /// <response code="422">Validasiya xətası.</response>
    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(SuccessResponse<DirectorResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create([FromForm] CreateDirectorDto dto, CancellationToken cancellationToken = default)
    {
        var response = await directorService.CreateAsync(dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Mövcud rəhbəri yeniləyir.
    /// </summary>
    /// <param name="id">Yenilənəcək rəhbərin unikal identifikatoru.</param>
    /// <param name="dto">Yenilənmiş rəhbər məlumatları.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Yenilənmiş rəhbər DTO-su.</returns>
    /// <response code="200">Rəhbər uğurla yeniləndi.</response>
    /// <response code="404">Rəhbər tapılmadı.</response>
    /// <response code="422">Validasiya xətası.</response>
    [HttpPut("{id:guid}")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(SuccessResponse<DirectorResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] UpdateDirectorDto dto, CancellationToken cancellationToken = default)
    {
        var response = await directorService.UpdateAsync(id, dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Rəhbəri aktivləşdirir.
    /// </summary>
    /// <param name="id">Rəhbərin unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Əməliyyatın nəticəsi.</returns>
    [HttpPatch("activate/{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Activate([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await directorService.ActivateAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Rəhbəri deaktivləşdirir (Soft delete).
    /// </summary>
    /// <param name="id">Rəhbərin unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Əməliyyatın nəticəsi.</returns>
    [HttpPatch("deactivate/{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Deactivate([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await directorService.DeActivateAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Rəhbəri həmişəlik silir.
    /// </summary>
    /// <param name="id">Silinəcək rəhbərin unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Əməliyyatın nəticəsi.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await directorService.DeleteAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }
}
