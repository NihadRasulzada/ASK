using App.API.Controllers.Common;
using App.API.Extensions;
using App.BL.DTOs;
using App.BL.Services.Business.ForeignRepresentatives;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ForeignRepresentativesController(IForeignRepresentativesService foreignRepresentativesService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(SuccessResponse<IEnumerable<ForeignRepresentativesResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var response = await foreignRepresentativesService.GetAllAsync(cancellationToken);
        return this.HandleServiceResponse(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse<ForeignRepresentativesResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await foreignRepresentativesService.GetByIdAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(SuccessResponse<ForeignRepresentativesResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create([FromBody] CreateForeignRepresentativesDto dto, CancellationToken cancellationToken = default)
    {
        var response = await foreignRepresentativesService.CreateAsync(dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse<ForeignRepresentativesResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateForeignRepresentativesDto dto, CancellationToken cancellationToken = default)
    {
        var response = await foreignRepresentativesService.UpdateAsync(id, dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await foreignRepresentativesService.DeleteAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }
}
