using App.API.Controllers.Common;
using App.API.Extensions;
using App.BL.DTOs;
using App.BL.Services.Business.OurValues;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class OurValuesController(IOurValuesService ourValuesService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(SuccessResponse<IEnumerable<OurValuesResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var response = await ourValuesService.GetAllAsync(cancellationToken);
        return this.HandleServiceResponse(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse<OurValuesResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await ourValuesService.GetByIdAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(SuccessResponse<OurValuesResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create([FromForm] CreateOurValuesDto dto, CancellationToken cancellationToken = default)
    {
        var response = await ourValuesService.CreateAsync(dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    [HttpPut("{id:guid}")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(SuccessResponse<OurValuesResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] UpdateOurValuesDto dto, CancellationToken cancellationToken = default)
    {
        var response = await ourValuesService.UpdateAsync(id, dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await ourValuesService.DeleteAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }
}
