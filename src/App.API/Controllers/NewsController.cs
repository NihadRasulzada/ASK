using App.BL.DTOs;
using App.BL.Services.Abstractions;
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
    /// Bütün xəbərlərin siyahısını qaytarır.
    /// </summary>
    /// <returns>Xəbər siyahısı.</returns>
    /// <response code="200">Xəbərlərin siyahısı uğurla qaytarıldı.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<NewsResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var newsList = await _newsService.GetAllAsync();
        return Ok(newsList);
    }

    /// <summary>
    /// Verilmiş identifikator üzrə bir xəbər qaytarır.
    /// </summary>
    /// <param name="id">Xəbərin unikal identifikatoru.</param>
    /// <returns>Tələb olunan xəbər.</returns>
    /// <response code="200">Xəbər uğurla tapıldı.</response>
    /// <response code="404">Xəbər tapılmadı.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(NewsResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var news = await _newsService.GetByIdAsync(id);
        if (news is null)
            return NotFound();

        return Ok(news);
    }

    /// <summary>
    /// Yeni xəbər yaradır. Başlıq şəkli və əlavə şəkillər Cloudinary-yə yüklənir.
    /// </summary>
    /// <param name="dto">Xəbər yaratmaq üçün form məlumatı.</param>
    /// <returns>Yaradılmış xəbər.</returns>
    /// <response code="201">Xəbər uğurla yaradıldı.</response>
    /// <response code="400">Validasiya xətası.</response>
    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(NewsResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromForm] CreateNewsDto dto)
    {
        var created = await _newsService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Mövcud xəbəri yeniləyir. Şəkillər göndərildikdə Cloudinary-yə yüklənir.
    /// </summary>
    /// <param name="id">Yenilənəcək xəbərin unikal identifikatoru.</param>
    /// <param name="dto">Xəbər yeniləmək üçün form məlumatı.</param>
    /// <returns>Yenilənmiş xəbər.</returns>
    /// <response code="200">Xəbər uğurla yeniləndi.</response>
    /// <response code="400">Validasiya xətası.</response>
    /// <response code="404">Xəbər tapılmadı.</response>
    [HttpPut("{id:guid}")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(NewsResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] UpdateNewsDto dto)
    {
        var updated = await _newsService.UpdateAsync(id, dto);
        if (updated is null)
            return NotFound();

        return Ok(updated);
    }

    /// <summary>
    /// Xəbəri identifikator üzrə silir.
    /// </summary>
    /// <param name="id">Silinəcək xəbərin unikal identifikatoru.</param>
    /// <returns>Uğur halında məzmun yoxdur.</returns>
    /// <response code="204">Xəbər uğurla silindi.</response>
    /// <response code="404">Xəbər tapılmadı.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deleted = await _newsService.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
