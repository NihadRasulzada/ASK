using App.API.Controllers.Common;
using App.Core.ResponseObject.Concreate;
using App.Core.ResponseObject.Enums;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Extensions;

public static class ControllerExtensions
{
    /// <summary>
    /// Handles service response with data
    /// </summary>
    public static IActionResult HandleServiceResponse<T>(
        this ControllerBase controller,
        Response<T> response)
    {
        return response.ResponseStatusCode switch
        {
            ResponseStatusCode.Success => controller.Ok(new SuccessResponse<T>(
                 data: response.Data,
                 message: response.Message ?? "Operation completed successfully"
            )),
            ResponseStatusCode.ValidationError => controller.UnprocessableEntity(new ValidationErrorResponse(
                response.Message ?? "Validation failed",
                response.ValidationErrors
            )),
            ResponseStatusCode.NotFound => controller.NotFound(new NotFoundResponse(response.Message)),
            ResponseStatusCode.BadRequest => controller.BadRequest(new BadRequestResponse(response.Message)),
            ResponseStatusCode.Error => controller.StatusCode(500, new ServerErrorResponse(
                response.Message ?? "An internal server error occurred",
                response.Errors
            )),
            _ => controller.StatusCode(500, new ErrorResponse(response.Message ?? "An internal server error occurred"))
        };
    }

    /// <summary>
    /// Handles service response without data
    /// </summary>
    public static IActionResult HandleServiceResponse(
        this ControllerBase controller,
        Response response)
    {
        return response.ResponseStatusCode switch
        {
            ResponseStatusCode.Success => controller.Ok(new SuccessResponse(
                 message: response.Message ?? "Operation completed successfully"
            )),
            ResponseStatusCode.ValidationError => controller.UnprocessableEntity(new ValidationErrorResponse(
                response.Message ?? "Validation failed",
                response.ValidationErrors
            )),
            ResponseStatusCode.NotFound => controller.NotFound(new NotFoundResponse(response.Message)),
            ResponseStatusCode.BadRequest => controller.BadRequest(new BadRequestResponse(response.Message)),
            ResponseStatusCode.Error => controller.StatusCode(500, new ServerErrorResponse(
                response.Message ?? "An internal server error occurred",
                response.Errors
            )),
            _ => controller.StatusCode(500, new ErrorResponse(response.Message ?? "An internal server error occurred"))
        };
    }

    /// <summary>
    /// Handles paginated service response
    /// </summary>
    public static IActionResult HandlePagedServiceResponse<T>(
        this ControllerBase controller,
        PagedResponse<T> response)
    {
        return response.ResponseStatusCode switch
        {
            ResponseStatusCode.Success => controller.Ok(new PagedDataResponse<T>(
                response.Data,
                response.Message,
                new PaginationMetadata(
                    response.PageIndex,
                    response.PageSize,
                    response.TotalCount,
                    response.TotalPages,
                    response.HasPreviousPage,
                    response.HasNextPage)
            )),
            ResponseStatusCode.ValidationError => controller.UnprocessableEntity(new ValidationErrorResponse(
                response.Message ?? "Validation failed",
                response.ValidationErrors
            )),
            ResponseStatusCode.NotFound => controller.NotFound(new NotFoundResponse(response.Message)),
            ResponseStatusCode.BadRequest => controller.BadRequest(new BadRequestResponse(response.Message)),
            ResponseStatusCode.Error => controller.StatusCode(500, new ServerErrorResponse(
                response.Message ?? "An internal server error occurred",
                response.Errors
            )),
            _ => controller.StatusCode(500, new ErrorResponse(response.Message ?? "An internal server error occurred"))
        };
    }

    public static IActionResult HandlePaginatedEnumerableResponse<T>(
        this ControllerBase controller,
        Response<IEnumerable<T>> response,
        int pageIndex,
        int pageSize)
    {
        if (response.ResponseStatusCode != ResponseStatusCode.Success)
            return controller.HandleServiceResponse(response);

        pageIndex = Math.Max(1, pageIndex);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var source = response.Data ?? Enumerable.Empty<T>();
        var totalCount = source.Count();
        var items = source
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var pagedResponse = PagedResponse<IEnumerable<T>>.Create(
            items,
            pageIndex,
            pageSize,
            totalCount,
            response.Message ?? "Items retrieved successfully");

        return controller.HandlePagedServiceResponse(pagedResponse);
    }
}
