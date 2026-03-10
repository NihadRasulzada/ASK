using App.BL.DTOs;
using App.BL.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

/// <summary>
/// Video resurslarını idarə edir.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class VideoController : ControllerBase
{
    private readonly IVideoService _videoService;

    /// <summary>
    /// VideoController-i başladır.
    /// </summary>
    /// <param name="videoService">Video servis instansiyası.</param>
    public VideoController(IVideoService videoService)
    {
        _videoService = videoService;
    }

    /// <summary>
    /// Bütün videoların siyahısını qaytarır.
    /// </summary>
    /// <returns>Video siyahısı.</returns>
    /// <response code="200">Videoların siyahısı uğurla qaytarıldı.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<VideoResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var videos = await _videoService.GetAllAsync();
        return Ok(videos);
    }

    /// <summary>
    /// Verilmiş identifikator üzrə bir video qaytarır.
    /// </summary>
    /// <param name="id">Videonun unikal identifikatoru.</param>
    /// <returns>Tələb olunan video.</returns>
    /// <response code="200">Video uğurla tapıldı.</response>
    /// <response code="404">Video tapılmadı.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(VideoResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var video = await _videoService.GetByIdAsync(id);
        if (video is null)
            return NotFound();

        return Ok(video);
    }

    /// <summary>
    /// Yeni video yaradır.
    /// </summary>
    /// <param name="dto">Video yaratmaq üçün məlumat.</param>
    /// <returns>Yaradılmış video.</returns>
    /// <response code="201">Video uğurla yaradıldı.</response>
    /// <response code="400">Validasiya xətası.</response>
    [HttpPost]
    [ProducesResponseType(typeof(VideoResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateVideoDto dto)
    {
        var created = await _videoService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Mövcud videonu yeniləyir.
    /// </summary>
    /// <param name="id">Yenilənəcək videonun unikal identifikatoru.</param>
    /// <param name="dto">Video yeniləmək üçün məlumat.</param>
    /// <returns>Yenilənmiş video.</returns>
    /// <response code="200">Video uğurla yeniləndi.</response>
    /// <response code="400">Validasiya xətası.</response>
    /// <response code="404">Video tapılmadı.</response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(VideoResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateVideoDto dto)
    {
        var updated = await _videoService.UpdateAsync(id, dto);
        if (updated is null)
            return NotFound();

        return Ok(updated);
    }

    /// <summary>
    /// Videonu identifikator üzrə silir.
    /// </summary>
    /// <param name="id">Silinəcək videonun unikal identifikatoru.</param>
    /// <returns>Uğur halında məzmun yoxdur.</returns>
    /// <response code="204">Video uğurla silindi.</response>
    /// <response code="404">Video tapılmadı.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deleted = await _videoService.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
