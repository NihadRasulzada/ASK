using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Middleware;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        => _logger = logger;

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Tutulmamış istisna: {Message}", exception.Message);

        var (statusCode, title) = exception switch
        {
            KeyNotFoundException => (StatusCodes.Status404NotFound, "Resurs tapılmadı"),
            ArgumentException => (StatusCodes.Status400BadRequest, "Yanlış sorğu"),
            UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "İcazəsiz giriş"),
            InvalidOperationException => (StatusCodes.Status400BadRequest, "Yanlış əməliyyat"),
            _ => (StatusCodes.Status500InternalServerError, "Gözlənilməz xəta baş verdi")
        };

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = exception.Message
        };

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
