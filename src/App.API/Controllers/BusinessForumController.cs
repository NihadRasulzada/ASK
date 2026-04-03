using App.API.Controllers.Common;
using App.API.Extensions;
using App.BL.DTOs;
using App.BL.Services.Business.BusinessForum;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

/// <summary>
/// Biznes Forum resurslarını idarə edir.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class BusinessForumController(IBusinessForumService businessForumService) : ControllerBase
{
    /// <summary>
    /// Biznes forumların səhifələnmiş siyahısını qaytarır.
    /// </summary>
    /// <param name="pageIndex">Səhifə nömrəsi (1-dən başlayır).</param>
    /// <param name="pageSize">Hər səhifədəki element sayı.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Biznes forum DTO-larının siyahısı.</returns>
    /// <response code="200">Siyahı uğurla qaytarıldı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpGet]
    [ProducesResponseType(typeof(PagedDataResponse<BusinessForumResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var response = await businessForumService.GetAllAsync(pageIndex, pageSize, cancellationToken);
        return this.HandlePagedServiceResponse(response);
    }

    /// <summary>
    /// Verilmiş ID-yə görə biznes forum məlumatlarını qaytarır.
    /// </summary>
    /// <param name="id">Biznes forumun unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Tapılan biznes forum DTO-su.</returns>
    /// <response code="200">Biznes forum uğurla tapıldı.</response>
    /// <response code="404">Biznes forum tapılmadı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse<BusinessForumResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await businessForumService.GetByIdAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Yeni biznes forum yaradır.
    /// </summary>
    /// <param name="dto">Yaradılacaq biznes forumun məlumatları (multipart/form-data).</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Yaradılmış biznes forum DTO-su.</returns>
    /// <response code="200">Biznes forum uğurla yaradıldı.</response>
    /// <response code="422">Validasiya xətası.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(SuccessResponse<BusinessForumResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromForm] CreateBusinessForumDto dto, CancellationToken cancellationToken = default)
    {
        var response = await businessForumService.CreateAsync(dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Mövcud biznes forumu yeniləyir.
    /// </summary>
    /// <param name="id">Yenilənəcək biznes forumun unikal identifikatoru.</param>
    /// <param name="dto">Yenilənmiş biznes forum məlumatları (multipart/form-data).</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Yenilənmiş biznes forum DTO-su.</returns>
    /// <response code="200">Biznes forum uğurla yeniləndi.</response>
    /// <response code="404">Biznes forum tapılmadı.</response>
    /// <response code="422">Validasiya xətası.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpPut("{id:guid}")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(SuccessResponse<BusinessForumResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] UpdateBusinessForumDto dto, CancellationToken cancellationToken = default)
    {
        var response = await businessForumService.UpdateAsync(id, dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Biznes forumu həmişəlik silir.
    /// </summary>
    /// <param name="id">Silinəcək biznes forumun unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Əməliyyatın nəticəsi.</returns>
    /// <response code="200">Biznes forum uğurla silindi.</response>
    /// <response code="404">Biznes forum tapılmadı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await businessForumService.DeleteAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }
}
