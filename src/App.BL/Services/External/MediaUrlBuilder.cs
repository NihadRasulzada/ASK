using Microsoft.AspNetCore.Http;

namespace App.BL.Services.External;

public class MediaUrlBuilder(IHttpContextAccessor httpContextAccessor) : IMediaUrlBuilder
{
    private const string CloudinaryBase = "https://res.cloudinary.com/";

    public string? Build(string? cloudinaryUrlOrPath)
    {
        if (string.IsNullOrEmpty(cloudinaryUrlOrPath)) return null;

        var path = ToRelativePath(cloudinaryUrlOrPath);

        var req = httpContextAccessor.HttpContext?.Request;
        return req is null
            ? $"/api/media/{path}"
            : $"{req.Scheme}://{req.Host}/api/media/{path}";
    }

    // "https://res.cloudinary.com/cloudname/image/upload/..." → "image/upload/..."
    // "image/upload/..."                                      → "image/upload/..." (toxunmaz)
    private static string ToRelativePath(string url)
    {
        if (!url.StartsWith(CloudinaryBase, StringComparison.OrdinalIgnoreCase))
            return url;

        var after = url[CloudinaryBase.Length..]; // "cloudname/image/upload/..."
        var slash = after.IndexOf('/');
        return slash < 0 ? after : after[(slash + 1)..]; // "image/upload/..."
    }
}
