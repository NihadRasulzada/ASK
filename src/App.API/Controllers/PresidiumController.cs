using App.API.Controllers.Common;
using App.API.Extensions;
using App.BL.DTOs;
using App.BL.Services.Business.Presidium;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PresidiumController(IPresidiumService presidiumService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(SuccessResponse<IEnumerable<PresidiumResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var response = await presidiumService.GetAllAsync(cancellationToken);
        return this.HandleServiceResponse(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse<PresidiumResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await presidiumService.GetByIdAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(SuccessResponse<PresidiumResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create([FromForm] CreatePresidiumDto dto, CancellationToken cancellationToken = default)
    {
        var response = await presidiumService.CreateAsync(dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    [HttpPut("{id:guid}")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(SuccessResponse<PresidiumResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] UpdatePresidiumDto dto, CancellationToken cancellationToken = default)
    {
        var response = await presidiumService.UpdateAsync(id, dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await presidiumService.DeleteAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }
}
