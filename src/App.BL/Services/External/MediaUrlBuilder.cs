using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace App.BL.Services.External;

public class MediaUrlBuilder(IHttpContextAccessor httpContextAccessor) : IMediaUrlBuilder
{
    private const string SeedMediaPrefix = "seed-media/";
    private const string SeedMediaApiPrefix = "/api/seed-media/";
    private const string MinioSeedPrefix = "wordpress-seed/";

    public string? Build(string? cloudinaryUrlOrPath)
    {
        if (string.IsNullOrEmpty(cloudinaryUrlOrPath)) return null;

        if (cloudinaryUrlOrPath.StartsWith(SeedMediaPrefix, StringComparison.OrdinalIgnoreCase))
            return Build($"{MinioSeedPrefix}{cloudinaryUrlOrPath[SeedMediaPrefix.Length..]}");

        if (cloudinaryUrlOrPath.StartsWith(SeedMediaApiPrefix, StringComparison.OrdinalIgnoreCase))
            return Build($"{MinioSeedPrefix}{cloudinaryUrlOrPath[SeedMediaApiPrefix.Length..]}");

        if (Uri.TryCreate(cloudinaryUrlOrPath, UriKind.Absolute, out var absoluteUri) &&
            (absoluteUri.Scheme == Uri.UriSchemeHttp || absoluteUri.Scheme == Uri.UriSchemeHttps))
            return cloudinaryUrlOrPath;

        var path = cloudinaryUrlOrPath.TrimStart('/');

        var req = httpContextAccessor.HttpContext?.Request;
        return req is null
            ? $"/api/media/{path}"
            : $"{req.Scheme}://{req.Host}/api/media/{path}";
    }

    public string BuildHtml(string? html)
    {
        if (string.IsNullOrEmpty(html)) return string.Empty;

        var result = Regex.Replace(
            html,
            "(?<attr>\\b(?:src|href)\\s*=\\s*[\"'])(?<path>seed-media/[^\"']+)",
            match => $"{match.Groups["attr"].Value}{Build(match.Groups["path"].Value)}",
            RegexOptions.IgnoreCase);

        return Regex.Replace(
            result,
            "(?<attr>\\b(?:src|href)\\s*=\\s*[\"'])(?<path>/api/seed-media/[^\"']+)",
            match => $"{match.Groups["attr"].Value}{Build(match.Groups["path"].Value)}",
            RegexOptions.IgnoreCase);
    }
}
