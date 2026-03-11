using App.Core.Interfaces;

namespace App.API.Middleware;

public class LanguageMiddleware(RequestDelegate next)
{
    private static readonly HashSet<string> _supported = ["az", "en", "ru"];

    public async Task InvokeAsync(HttpContext context, ILanguageService languageService)
    {
        var lang = context.Request.Query["lang"].ToString();
        languageService.Lang = _supported.Contains(lang) ? lang : "az";
        await next(context);
    }
}
