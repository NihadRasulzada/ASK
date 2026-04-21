using App.API.Controllers.Common;
using App.API.Extensions;
using App.BL.DTOs;
using App.BL.Services.Business.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

/// <summary>
/// Xidmət resurslarını idarə edir.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ServiceController : ControllerBase
{
    private readonly IServiceService _serviceService;

    /// <summary>
    /// ServiceController-i başladır.
    /// </summary>
    /// <param name="serviceService">Xidmət servis instansiyası.</param>
    public ServiceController(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    /// <summary>
    /// Bütün aktiv xidmətlərin siyahısını qaytarır.
    /// </summary>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Xidmət DTO-larının siyahısı.</returns>
    /// <response code="200">Xidmətlər uğurla qaytarıldı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpGet]
    [ProducesResponseType(typeof(SuccessResponse<IEnumerable<ServiceResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var response = await _serviceService.GetAllAsync(cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Silinmiş xidmətlər daxil olmaqla bütün xidmətləri qaytarır.
    /// </summary>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Bütün xidmət DTO-larının siyahısı.</returns>
    /// <response code="200">Xidmətlər uğurla qaytarıldı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpGet("including-deleted")]
    [ProducesResponseType(typeof(SuccessResponse<IEnumerable<ServiceResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllIncludingDeleted(CancellationToken cancellationToken = default)
    {
        var response = await _serviceService.GetAllIncludingDeletedAsync(cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Verilmiş ID-yə görə xidməti qaytarır.
    /// </summary>
    /// <param name="id">Xidmətin unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Tapılan xidmət DTO-su.</returns>
    /// <response code="200">Xidmət uğurla tapıldı.</response>
    /// <response code="404">Xidmət tapılmadı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse<ServiceResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _serviceService.GetByIdAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Yeni xidmət yaradır.
    /// </summary>
    /// <param name="dto">Yaradılacaq xidmətin məlumatları (multipart/form-data).</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Əməliyyatın nəticəsi.</returns>
    /// <response code="200">Xidmət uğurla yaradıldı.</response>
    /// <response code="422">Validasiya xətası.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpPost]
    [Authorize]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromForm] CreateServiceDto dto, CancellationToken cancellationToken = default)
    {
        var response = await _serviceService.CreateAsync(dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Mövcud xidməti yeniləyir.
    /// </summary>
    /// <param name="dto">Yenilənmiş xidmət məlumatları, ID daxil (multipart/form-data).</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Yenilənmiş xidmət DTO-su.</returns>
    /// <response code="200">Xidmət uğurla yeniləndi.</response>
    /// <response code="404">Xidmət tapılmadı.</response>
    /// <response code="422">Validasiya xətası.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpPut]
    [Authorize]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(SuccessResponse<ServiceResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(Guid id, [FromForm] UpdateServiceDto dto, CancellationToken cancellationToken = default)
    {
        var response = await _serviceService.UpdateAsync(id, dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Xidməti aktivləşdirir.
    /// </summary>
    /// <param name="id">Aktivləşdiriləcək xidmətin unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Əməliyyatın nəticəsi.</returns>
    /// <response code="200">Xidmət uğurla aktivləşdirildi.</response>
    /// <response code="400">Xidmət artıq aktivdir.</response>
    /// <response code="404">Xidmət tapılmadı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpPatch("{id:guid}/activate")]
    [Authorize]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Activate([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _serviceService.ActivateAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Xidməti deaktivləşdirir.
    /// </summary>
    /// <param name="id">Deaktivləşdiriləcək xidmətin unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Əməliyyatın nəticəsi.</returns>
    /// <response code="200">Xidmət uğurla deaktivləşdirildi.</response>
    /// <response code="400">Xidmət artıq deaktivdir.</response>
    /// <response code="404">Xidmət tapılmadı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpPatch("{id:guid}/deactivate")]
    [Authorize]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Deactivate([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _serviceService.DeActivateAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Xidməti həmişəlik silir.
    /// </summary>
    /// <param name="id">Silinəcək xidmətin unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Əməliyyatın nəticəsi.</returns>
    /// <response code="200">Xidmət uğurla silindi.</response>
    /// <response code="404">Xidmət tapılmadı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpDelete("{id:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _serviceService.DeleteAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }
}
