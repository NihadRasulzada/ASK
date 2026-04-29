using App.API.Controllers.Common;
using App.BL.Resources;
using App.Core.Interfaces;
using App.Core.ResponseObject;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Middleware;

public class FileSizeLimitMiddleware(RequestDelegate next)
{
    private const long MaxFileSize = 10 * 1024 * 1024; // 10 MB

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Method == HttpMethods.Options)
        {
            await next(context);
            return;
        }

        if (context.Request.HasFormContentType)
        {
            var contentLength = context.Request.ContentLength;
            if (contentLength.HasValue && contentLength.Value > MaxFileSize)
            {
                if (context.Response.HasStarted) return;

                context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                await context.Response.WriteAsJsonAsync(new ValidationErrorResponse(
                    "Validasiya xətası.",
                    [
                        new CustomValidationError("file","Fayl ölçüsü 10 MB-dan böyük ola bilməz.")
                    ]
                ));
                return;
            }
        }

        await next(context);
    }
}