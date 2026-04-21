using App.API.Controllers.Common;
using App.API.Extensions;
using App.BL.DTOs;
using App.BL.Services.Business.News;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

/// <summary>
/// Xəbər resurslarını idarə edir.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class NewsController : ControllerBase
{
    private readonly INewsService _newsService;

    /// <summary>
    /// NewsController-i başladır.
    /// </summary>
    /// <param name="newsService">Xəbər servis instansiyası.</param>
    public NewsController(INewsService newsService)
    {
        _newsService = newsService;
    }

    /// <summary>
    /// Bütün aktiv xəbərlərin siyahısını qaytarır.
    /// </summary>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Xəbər DTO-larının siyahısı.</returns>
    /// <response code="200">Xəbərlər uğurla qaytarıldı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpGet]
    [ProducesResponseType(typeof(SuccessResponse<IEnumerable<NewsResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var response = await _newsService.GetAllAsync(cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Silinmiş xəbərlər daxil olmaqla bütün xəbərləri qaytarır.
    /// </summary>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Bütün xəbər DTO-larının siyahısı.</returns>
    /// <response code="200">Xəbərlər uğurla qaytarıldı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpGet("including-deleted")]
    [ProducesResponseType(typeof(SuccessResponse<IEnumerable<NewsResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllIncludingDeleted(CancellationToken cancellationToken = default)
    {
        var response = await _newsService.GetAllIncludingDeletedAsync(cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Verilmiş ID-yə görə xəbəri qaytarır.
    /// </summary>
    /// <param name="id">Xəbərin unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Tapılan xəbər DTO-su.</returns>
    /// <response code="200">Xəbər uğurla tapıldı.</response>
    /// <response code="404">Xəbər tapılmadı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse<NewsResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _newsService.GetByIdAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Yeni xəbər yaradır.
    /// </summary>
    /// <param name="dto">Yaradılacaq xəbərin məlumatları (multipart/form-data).</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Əməliyyatın nəticəsi.</returns>
    /// <response code="200">Xəbər uğurla yaradıldı.</response>
    /// <response code="422">Validasiya xətası.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpPost]
    [Authorize]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromForm] CreateNewsDto dto, CancellationToken cancellationToken = default)
    {
        var response = await _newsService.CreateAsync(dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Mövcud xəbəri yeniləyir.
    /// </summary>
    /// <param name="id">Yenilənəcək xəbərin unikal identifikatoru.</param>
    /// <param name="dto">Yenilənmiş xəbər məlumatları (multipart/form-data).</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Yenilənmiş xəbər DTO-su.</returns>
    /// <response code="200">Xəbər uğurla yeniləndi.</response>
    /// <response code="404">Xəbər tapılmadı.</response>
    /// <response code="422">Validasiya xətası.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpPut("{id:guid}")]
    [Authorize]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(SuccessResponse<NewsResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] UpdateNewsDto dto, CancellationToken cancellationToken = default)
    {
        var response = await _newsService.UpdateAsync(id, dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Xəbəri bərpa edir (aktivləşdirir).
    /// </summary>
    /// <param name="id">Aktivləşdiriləcək xəbərin unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Əməliyyatın nəticəsi.</returns>
    /// <response code="200">Xəbər uğurla aktivləşdirildi.</response>
    /// <response code="400">Xəbər artıq aktivdir.</response>
    /// <response code="404">Xəbər tapılmadı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpPatch("{id:guid}/activate")]
    [Authorize]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Activate([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _newsService.ActivateAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Xəbəri deaktivləşdirir.
    /// </summary>
    /// <param name="id">Deaktivləşdiriləcək xəbərin unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Əməliyyatın nəticəsi.</returns>
    /// <response code="200">Xəbər uğurla deaktivləşdirildi.</response>
    /// <response code="400">Xəbər artıq deaktivdir.</response>
    /// <response code="404">Xəbər tapılmadı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpPatch("{id:guid}/deactivate")]
    [Authorize]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Deactivate([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _newsService.DeActivateAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Xəbəri həmişəlik silir.
    /// </summary>
    /// <param name="id">Silinəcək xəbərin unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Əməliyyatın nəticəsi.</returns>
    /// <response code="200">Xəbər uğurla silindi.</response>
    /// <response code="404">Xəbər tapılmadı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpDelete("{id:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _newsService.DeleteAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }
}
