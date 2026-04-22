using App.API.Controllers.Common;
using App.API.Extensions;
using App.BL.DTOs;
using App.BL.Services.Business.Training;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

/// <summary>
/// Təlimlər resurslarını idarə edir.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class TrainingController(ITrainingService trainingService) : ControllerBase
{
    /// <summary>
    /// Aktiv təlimlərin siyahısını qaytarır.
    /// </summary>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Təlim DTO-larının siyahısı.</returns>
    /// <response code="200">Siyahı uğurla qaytarıldı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpGet]
    [ProducesResponseType(typeof(PagedDataResponse<TrainingResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var response = await trainingService.GetAllAsync(pageIndex, pageSize, cancellationToken);
        return this.HandlePagedServiceResponse(response);
    }

    /// <summary>
    /// Bütün təlimlərin (silinmişlər daxil olmaqla) səhifələnmiş siyahısını qaytarır.
    /// </summary>
    /// <param name="pageIndex">Səhifə nömrəsi (1-dən başlayır).</param>
    /// <param name="pageSize">Hər səhifədəki element sayı.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Təlim DTO-larının tam siyahısı.</returns>
    /// <response code="200">Siyahı uğurla qaytarıldı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpGet("all")]
    [ProducesResponseType(typeof(PagedDataResponse<TrainingResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllIncludingDeleted([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var response = await trainingService.GetAllIncludingDeletedAsync(pageIndex, pageSize, cancellationToken);
        return this.HandlePagedServiceResponse(response);
    }

    /// <summary>
    /// Verilmiş ID-yə görə təlim məlumatlarını qaytarır.
    /// </summary>
    /// <param name="id">Təlimin unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Tapılan təlim DTO-su.</returns>
    /// <response code="200">Təlim uğurla tapıldı.</response>
    /// <response code="404">Təlim tapılmadı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse<TrainingResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await trainingService.GetByIdAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Yeni təlim yaradır.
    /// </summary>
    /// <param name="dto">Yaradılacaq təlimin məlumatları (multipart/form-data).</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Yaradılmış təlim DTO-su.</returns>
    /// <response code="200">Təlim uğurla yaradıldı.</response>
    /// <response code="422">Validasiya xətası.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpPost]
    [Authorize]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(SuccessResponse<TrainingResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromForm] CreateTrainingDto dto, CancellationToken cancellationToken = default)
    {
        var response = await trainingService.CreateAsync(dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Mövcud təlimi yeniləyir.
    /// </summary>
    /// <param name="id">Yenilənəcək təlimin unikal identifikatoru.</param>
    /// <param name="dto">Yenilənmiş təlim məlumatları (multipart/form-data).</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Yenilənmiş təlim DTO-su.</returns>
    /// <response code="200">Təlim uğurla yeniləndi.</response>
    /// <response code="404">Təlim tapılmadı.</response>
    /// <response code="422">Validasiya xətası.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpPut("{id:guid}")]
    [Authorize]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(SuccessResponse<TrainingResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] UpdateTrainingDto dto, CancellationToken cancellationToken = default)
    {
        var response = await trainingService.UpdateAsync(id, dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Təlimi aktivləşdirir.
    /// </summary>
    /// <param name="id">Aktivləşdiriləcək təlimin unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Əməliyyatın nəticəsi.</returns>
    /// <response code="200">Təlim uğurla aktivləşdirildi.</response>
    /// <response code="400">Təlim artıq aktivdir.</response>
    /// <response code="404">Təlim tapılmadı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpPatch("activate/{id:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Activate([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await trainingService.ActivateAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Təlimi deaktivləşdirir (Soft delete).
    /// </summary>
    /// <param name="id">Deaktivləşdiriləcək təlimin unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Əməliyyatın nəticəsi.</returns>
    /// <response code="200">Təlim uğurla deaktivləşdirildi.</response>
    /// <response code="400">Təlim artıq deaktivdir.</response>
    /// <response code="404">Təlim tapılmadı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpPatch("deactivate/{id:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Deactivate([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await trainingService.DeActivateAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Təlimi həmişəlik silir.
    /// </summary>
    /// <param name="id">Silinəcək təlimin unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Əməliyyatın nəticəsi.</returns>
    /// <response code="200">Təlim uğurla silindi.</response>
    /// <response code="404">Təlim tapılmadı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpDelete("{id:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await trainingService.DeleteAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }
}
