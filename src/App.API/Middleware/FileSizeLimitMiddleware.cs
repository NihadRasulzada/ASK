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

                var form = await context.Request.ReadFormAsync();
                var errors = new List<CustomValidationError>();

                foreach (var file in form.Files)
                {
                    if (file.Length > MaxFileSize)
                    {
                        errors.Add(new CustomValidationError(
                            file.Name, 
                            "Fayl ölçüsü 10 MB-dan böyük ola bilməz."
                        ));
                    }
                }

                if (errors.Count > 0)
                {
                    context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                    await context.Response.WriteAsJsonAsync(new ValidationErrorResponse(
                        "Validasiya xətası.",
                        errors
                    ));
                    return;
                }
            }
        }

        await next(context);
    }
}