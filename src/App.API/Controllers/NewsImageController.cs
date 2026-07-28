using App.API.Controllers.Common;
using App.API.Extensions;
using App.BL.DTOs;
using App.BL.NewsImages.Business.NewsIamge;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

/// <summary>
/// Xəbər şəkilləri resurslarını idarə edir.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class NewsImageController : ControllerBase
{
    private readonly INewsImageService _newsImageService;

    /// <summary>
    /// NewsImageController-i başladır.
    /// </summary>
    /// <param name="newsImageService">Xəbər şəkili servis instansiyası.</param>
    public NewsImageController(INewsImageService newsImageService)
    {
        _newsImageService = newsImageService;
    }

    /// <summary>
    /// Bütün xəbər şəkillərinin siyahısını qaytarır.
    /// </summary>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Xəbər şəkili DTO-larının siyahısı.</returns>
    /// <response code="200">Şəkillər uğurla qaytarıldı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpGet]
    [ProducesResponseType(typeof(PagedDataResponse<IEnumerable<NewsImageResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var response = await _newsImageService.GetAllAsync(cancellationToken);
        return this.HandlePaginatedEnumerableResponse(response, pageIndex, pageSize);
    }

    /// <summary>
    /// Verilmiş ID-yə görə xəbər şəkilini qaytarır.
    /// </summary>
    /// <param name="id">Xəbər şəkilinin unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Tapılan xəbər şəkili DTO-su.</returns>
    /// <response code="200">Şəkil uğurla tapıldı.</response>
    /// <response code="404">Şəkil tapılmadı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse<NewsImageResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _newsImageService.GetByIdAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Xəbərə yeni şəkil əlavə edir.
    /// </summary>
    /// <param name="dto">Şəkil faylı və bağlı olduğu xəbərin ID-si (multipart/form-data).</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Əməliyyatın nəticəsi.</returns>
    /// <response code="200">Şəkil uğurla əlavə edildi.</response>
    /// <response code="422">Validasiya xətası.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpPost]
    [Authorize]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromForm] CreateNewsImageDto dto, CancellationToken cancellationToken = default)
    {
        var response = await _newsImageService.CreateAsync(dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Mövcud xəbər şəkilini yeniləyir.
    /// </summary>
    /// <param name="dto">Şəkil ID-si və yeni şəkil faylı (multipart/form-data).</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Yenilənmiş xəbər şəkili DTO-su.</returns>
    /// <response code="200">Şəkil uğurla yeniləndi.</response>
    /// <response code="404">Şəkil tapılmadı.</response>
    /// <response code="422">Validasiya xətası.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpPut]
    [Authorize]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(SuccessResponse<NewsImageResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(Guid id, [FromForm] UpdateNewsImageDto dto, CancellationToken cancellationToken = default)
    {
        var response = await _newsImageService.UpdateAsync(id, dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Xəbər şəkilini həmişəlik silir.
    /// </summary>
    /// <remarks>
    /// Diqqət: Şəkil həm DB-dən, həm də object storage-dən silinməlidir.
    /// </remarks>
    /// <param name="id">Silinəcək xəbər şəkilinin unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Əməliyyatın nəticəsi.</returns>
    /// <response code="200">Şəkil uğurla silindi.</response>
    /// <response code="404">Şəkil tapılmadı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpDelete("{id:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _newsImageService.DeleteAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }
}
