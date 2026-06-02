using Microsoft.AspNetCore.Http;

namespace App.BL.Services.External;

public class MediaUrlBuilder(IHttpContextAccessor httpContextAccessor) : IMediaUrlBuilder
{
    public string? Build(string? objectKey)
    {
        if (string.IsNullOrEmpty(objectKey)) return null;
        var req = httpContextAccessor.HttpContext?.Request;
        return req is null
            ? $"/api/media/{objectKey}"
            : $"{req.Scheme}://{req.Host}/api/media/{objectKey}";
    }
}
