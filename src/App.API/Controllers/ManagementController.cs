using App.API.Controllers.Common;
using App.API.Extensions;
using App.BL.DTOs;
using App.BL.Services.Business.Management;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ManagementController(IManagementService managementService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(SuccessResponse<IEnumerable<ManagementResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var response = await managementService.GetAllAsync(cancellationToken);
        return this.HandleServiceResponse(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse<ManagementResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await managementService.GetByIdAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(SuccessResponse<ManagementResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create([FromBody] CreateManagementDto dto, CancellationToken cancellationToken = default)
    {
        var response = await managementService.CreateAsync(dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse<ManagementResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateManagementDto dto, CancellationToken cancellationToken = default)
    {
        var response = await managementService.UpdateAsync(id, dto, cancellationToken);
        return this.HandleServiceResponse(response);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(SuccessResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var response = await managementService.DeleteAsync(id, cancellationToken);
        return this.HandleServiceResponse(response);
    }
}
