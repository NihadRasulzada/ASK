using App.API.Middleware;

namespace App.API.Extensions.FileSize;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseFileSizeLimit(this IApplicationBuilder app)
    {
        return app.UseMiddleware<FileSizeLimitMiddleware>();
    }
}