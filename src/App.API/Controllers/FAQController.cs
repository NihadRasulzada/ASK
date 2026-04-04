using App.API.Controllers.Common;
using System.Collections.Generic;
using App.API.Extensions;
using App.BL.DTOs;
using App.BL.Services.Business.FAQ;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

/// <summary>
/// Tez-tez verilən suallar (FAQ) resurslarını idarə edir.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class FAQController(IFAQService faqService) : ControllerBase
{
    /// <summary>
    /// Aktiv FAQ-ların siyahısını qaytarır.
    /// </summary>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>FAQ DTO-larının siyahısı.</returns>
    /// <response code="200">Siyahı uğurla qaytarıldı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpGet]
    [ProducesResponseType(typeof(SuccessResponse<IEnumerable<FAQResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var response = await faqService.GetAllAsync(cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Bütün FAQ-ların (silinmişlər daxil olmaqla) siyahısını qaytarır.
    /// </summary>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>FAQ DTO-larının tam siyahısı.</returns>
    /// <response code="200">Siyahı uğurla qaytarıldı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpGet("all")]
    [ProducesResponseType(typeof(SuccessResponse<IEnumerable<FAQResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllIncludingDeleted(CancellationToken cancellationToken = default)
    {
        var response = await faqService.GetAllIncludingDeletedAsync(cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Verilmiş ID-yə görə FAQ məlumatlarını qaytarır.
    /// </summary>
    /// <param name="id">FAQ-ın unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Tapılan FAQ DTO-su.</returns>
    /// <response code="200">FAQ uğurla tapıldı.</response>
    /// <response code="404">FAQ tapılmadı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse<FAQResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await faqService.GetByIdAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Yeni FAQ yaradır.
    /// </summary>
    /// <param name="dto">Yaradılacaq FAQ-ın məlumatları.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Yaradılmış FAQ DTO-su.</returns>
    /// <response code="200">FAQ uğurla yaradıldı.</response>
    /// <response code="422">Validasiya xətası.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpPost]
    [ProducesResponseType(typeof(SuccessResponse<FAQResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateFAQDto dto, CancellationToken cancellationToken = default)
    {
        var response = await faqService.CreateAsync(dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Mövcud FAQ-ı yeniləyir.
    /// </summary>
    /// <param name="id">Yenilənəcək FAQ-ın unikal identifikatoru.</param>
    /// <param name="dto">Yenilənmiş FAQ məlumatları.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Yenilənmiş FAQ DTO-su.</returns>
    /// <response code="200">FAQ uğurla yeniləndi.</response>
    /// <response code="404">FAQ tapılmadı.</response>
    /// <response code="422">Validasiya xətası.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse<FAQResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateFAQDto dto, CancellationToken cancellationToken = default)
    {
        var response = await faqService.UpdateAsync(id, dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// FAQ-ı aktivləşdirir.
    /// </summary>
    /// <param name="id">Aktivləşdiriləcək FAQ-ın unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Əməliyyatın nəticəsi.</returns>
    /// <response code="200">FAQ uğurla aktivləşdirildi.</response>
    /// <response code="400">FAQ artıq aktivdir.</response>
    /// <response code="404">FAQ tapılmadı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpPatch("activate/{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Activate([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await faqService.ActivateAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// FAQ-ı deaktivləşdirir (Soft delete).
    /// </summary>
    /// <param name="id">Deaktivləşdiriləcək FAQ-ın unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Əməliyyatın nəticəsi.</returns>
    /// <response code="200">FAQ uğurla deaktivləşdirildi.</response>
    /// <response code="400">FAQ artıq deaktivdir.</response>
    /// <response code="404">FAQ tapılmadı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpPatch("deactivate/{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Deactivate([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await faqService.DeActivateAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// FAQ-ı həmişəlik silir.
    /// </summary>
    /// <param name="id">Silinəcək FAQ-ın unikal identifikatoru.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Əməliyyatın nəticəsi.</returns>
    /// <response code="200">FAQ uğurla silindi.</response>
    /// <response code="404">FAQ tapılmadı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await faqService.DeleteAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// İstifadəçidən sual qəbul edir (sorgu).
    /// </summary>
    /// <param name="dto">Yalnız sual mətni tələb olunur.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Qeydə alınmış sorgu DTO-su.</returns>
    /// <response code="200">Sorgu uğurla qəbul edildi.</response>
    /// <response code="422">Validasiya xətası.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpPost("inquiry")]
    [ProducesResponseType(typeof(SuccessResponse<FAQInquiryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SubmitInquiry([FromBody] SubmitFAQInquiryDto dto, CancellationToken cancellationToken = default)
    {
        var response = await faqService.SubmitInquiryAsync(dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    /// <summary>
    /// Göndərilmiş sorguların səhifələnmiş siyahısını qaytarır.
    /// </summary>
    /// <param name="pageIndex">Səhifə nömrəsi (1-dən başlayır).</param>
    /// <param name="pageSize">Hər səhifədəki element sayı.</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Sorgu DTO-larının siyahısı.</returns>
    /// <response code="200">Siyahı uğurla qaytarıldı.</response>
    /// <response code="500">Server xətası baş verdi.</response>
    [HttpGet("inquiry")]
    [ProducesResponseType(typeof(PagedDataResponse<FAQInquiryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServerErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetInquiries([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var response = await faqService.GetInquiriesAsync(pageIndex, pageSize, cancellationToken);
        return this.HandlePagedServiceResponse(response);
    }
}
