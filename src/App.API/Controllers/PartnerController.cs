using App.BL.DTOs;
using App.BL.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

/// <summary>
/// Partner resurslarını idarə edir.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PartnerController : ControllerBase
{
    private readonly IPartnerService _partnerService;

    /// <summary>
    /// PartnerController-i başladır.
    /// </summary>
    /// <param name="partnerService">Partner servis instansiyası.</param>
    public PartnerController(IPartnerService partnerService)
    {
        _partnerService = partnerService;
    }

    /// <summary>
    /// Bütün partnerlərin siyahısını qaytarır.
    /// </summary>
    /// <returns>Partner siyahısı.</returns>
    /// <response code="200">Partnerlərin siyahısı uğurla qaytarıldı.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PartnerResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var partners = await _partnerService.GetAllAsync();
        return Ok(partners);
    }

    /// <summary>
    /// Verilmiş identifikator üzrə bir partner qaytarır.
    /// </summary>
    /// <param name="id">Partnerin unikal identifikatoru.</param>
    /// <returns>Tələb olunan partner.</returns>
    /// <response code="200">Partner uğurla tapıldı.</response>
    /// <response code="404">Partner tapılmadı.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PartnerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var partner = await _partnerService.GetByIdAsync(id);
        if (partner is null)
            return NotFound();

        return Ok(partner);
    }

    /// <summary>
    /// Yeni partner yaradır. Şəkil Cloudinary-yə yüklənir.
    /// </summary>
    /// <param name="dto">Partner yaratmaq üçün form məlumatı (şəkil + sayt URL-i).</param>
    /// <returns>Yaradılmış partner.</returns>
    /// <response code="201">Partner uğurla yaradıldı.</response>
    /// <response code="400">Validasiya xətası.</response>
    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(PartnerResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromForm] CreatePartnerDto dto)
    {
        var created = await _partnerService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Mövcud partneri yeniləyir. Şəkil göndərildikdə Cloudinary-yə yüklənir.
    /// </summary>
    /// <param name="id">Yenilənəcək partnerin unikal identifikatoru.</param>
    /// <param name="dto">Partner yeniləmək üçün form məlumatı.</param>
    /// <returns>Yenilənmiş partner.</returns>
    /// <response code="200">Partner uğurla yeniləndi.</response>
    /// <response code="400">Validasiya xətası.</response>
    /// <response code="404">Partner tapılmadı.</response>
    [HttpPut("{id:guid}")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(PartnerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] UpdatePartnerDto dto)
    {
        var updated = await _partnerService.UpdateAsync(id, dto);
        if (updated is null)
            return NotFound();

        return Ok(updated);
    }

    /// <summary>
    /// Partneri identifikator üzrə silir.
    /// </summary>
    /// <param name="id">Silinəcək partnerin unikal identifikatoru.</param>
    /// <returns>Uğur halında məzmun yoxdur.</returns>
    /// <response code="204">Partner uğurla silindi.</response>
    /// <response code="404">Partner tapılmadı.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deleted = await _partnerService.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
