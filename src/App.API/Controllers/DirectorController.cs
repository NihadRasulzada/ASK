using App.BL.DTOs;
using App.BL.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

/// <summary>
/// Direktor resurslarını idarə edir.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class DirectorController : ControllerBase
{
    private readonly IDirectorService _directorService;

    /// <summary>
    /// DirectorController-i başladır.
    /// </summary>
    /// <param name="directorService">Direktor servis instansiyası.</param>
    public DirectorController(IDirectorService directorService)
    {
        _directorService = directorService;
    }

    /// <summary>
    /// Bütün direktorların siyahısını qaytarır.
    /// </summary>
    /// <returns>Direktor siyahısı.</returns>
    /// <response code="200">Direktorların siyahısı uğurla qaytarıldı.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DirectorResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var directors = await _directorService.GetAllAsync();
        return Ok(directors);
    }

    /// <summary>
    /// Verilmiş identifikator üzrə bir direktor qaytarır.
    /// </summary>
    /// <param name="id">Direktorun unikal identifikatoru.</param>
    /// <returns>Tələb olunan direktor.</returns>
    /// <response code="200">Direktor uğurla tapıldı.</response>
    /// <response code="404">Direktor tapılmadı.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(DirectorResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var director = await _directorService.GetByIdAsync(id);
        if (director is null)
            return NotFound();

        return Ok(director);
    }

    /// <summary>
    /// Yeni direktor yaradır. Şəkil Cloudinary-yə yüklənir.
    /// </summary>
    /// <param name="dto">Direktor yaratmaq üçün form məlumatı.</param>
    /// <returns>Yaradılmış direktor.</returns>
    /// <response code="201">Direktor uğurla yaradıldı.</response>
    /// <response code="400">Validasiya xətası.</response>
    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(DirectorResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromForm] CreateDirectorDto dto)
    {
        var created = await _directorService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Mövcud direktoru yeniləyir. Şəkil göndərildikdə Cloudinary-yə yüklənir.
    /// </summary>
    /// <param name="id">Yenilənəcək direktorun unikal identifikatoru.</param>
    /// <param name="dto">Direktor yeniləmək üçün form məlumatı.</param>
    /// <returns>Yenilənmiş direktor.</returns>
    /// <response code="200">Direktor uğurla yeniləndi.</response>
    /// <response code="400">Validasiya xətası.</response>
    /// <response code="404">Direktor tapılmadı.</response>
    [HttpPut("{id:guid}")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(DirectorResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] UpdateDirectorDto dto)
    {
        var updated = await _directorService.UpdateAsync(id, dto);
        if (updated is null)
            return NotFound();

        return Ok(updated);
    }

    /// <summary>
    /// Direktoru identifikator üzrə silir.
    /// </summary>
    /// <param name="id">Silinəcək direktorun unikal identifikatoru.</param>
    /// <returns>Uğur halında məzmun yoxdur.</returns>
    /// <response code="204">Direktor uğurla silindi.</response>
    /// <response code="404">Direktor tapılmadı.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deleted = await _directorService.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
