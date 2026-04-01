using App.API.Controllers.Common;
using App.API.Extensions;
using App.BL.DTOs;
using App.BL.Services.Business.Committee;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CommitteeController(ICommitteeService committeeService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(SuccessResponse<IEnumerable<CommitteeResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var response = await committeeService.GetAllAsync(cancellationToken);
        return this.HandleServiceResponse(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse<CommitteeResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await committeeService.GetByIdAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(SuccessResponse<CommitteeResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create([FromBody] CreateCommitteeDto dto, CancellationToken cancellationToken = default)
    {
        var response = await committeeService.CreateAsync(dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse<CommitteeResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCommitteeDto dto, CancellationToken cancellationToken = default)
    {
        var response = await committeeService.UpdateAsync(id, dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await committeeService.DeleteAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }
}
