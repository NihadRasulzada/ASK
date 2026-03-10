using App.BL.DTOs;
using App.BL.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

/// <summary>
/// Prezident resursunu idarə edir. DB-də yalnız bir prezident qeydi ola bilər.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PresidentController : ControllerBase
{
    private readonly IPresidentService _presidentService;

    /// <summary>
    /// PresidentController-i başladır.
    /// </summary>
    /// <param name="presidentService">Prezident servis instansiyası.</param>
    public PresidentController(IPresidentService presidentService)
    {
        _presidentService = presidentService;
    }

    /// <summary>
    /// Mövcud prezident məlumatını qaytarır.
    /// </summary>
    /// <returns>Prezident məlumatı.</returns>
    /// <response code="200">Prezident məlumatı uğurla qaytarıldı.</response>
    /// <response code="404">Prezident məlumatı tapılmadı.</response>
    [HttpGet]
    [ProducesResponseType(typeof(PresidentResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get()
    {
        var president = await _presidentService.GetAsync();
        if (president is null)
            return NotFound();

        return Ok(president);
    }

    /// <summary>
    /// Prezident məlumatı yaradır. Şəkil Cloudinary-yə yüklənir.
    /// DB-də artıq prezident varsa 409 Conflict qaytarır.
    /// </summary>
    /// <param name="dto">Prezident yaratmaq üçün form məlumatı.</param>
    /// <returns>Yaradılmış prezident məlumatı.</returns>
    /// <response code="201">Prezident məlumatı uğurla yaradıldı.</response>
    /// <response code="400">Validasiya xətası.</response>
    /// <response code="409">DB-də artıq bir prezident qeydi mövcuddur.</response>
    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(PresidentResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromForm] CreatePresidentDto dto)
    {
        var created = await _presidentService.CreateAsync(dto);
        if (created is null)
            return Conflict("DB-də artıq bir prezident qeydi mövcuddur.");

        return CreatedAtAction(nameof(Get), created);
    }

    /// <summary>
    /// Mövcud prezident məlumatını yeniləyir. Şəkil göndərildikdə Cloudinary-yə yüklənir.
    /// </summary>
    /// <param name="dto">Prezident yeniləmək üçün form məlumatı.</param>
    /// <returns>Yenilənmiş prezident məlumatı.</returns>
    /// <response code="200">Prezident məlumatı uğurla yeniləndi.</response>
    /// <response code="400">Validasiya xətası.</response>
    /// <response code="404">Prezident məlumatı tapılmadı.</response>
    [HttpPut]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(PresidentResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromForm] UpdatePresidentDto dto)
    {
        var updated = await _presidentService.UpdateAsync(dto);
        if (updated is null)
            return NotFound();

        return Ok(updated);
    }

    /// <summary>
    /// Prezident məlumatını DB-dən silir.
    /// </summary>
    /// <returns>Uğur halında məzmun yoxdur.</returns>
    /// <response code="204">Prezident məlumatı uğurla silindi.</response>
    /// <response code="404">Prezident məlumatı tapılmadı.</response>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete()
    {
        var deleted = await _presidentService.DeleteAsync();
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
