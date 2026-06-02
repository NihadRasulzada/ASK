using App.BL.Services.External;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

/// <summary>
/// MinIO-dan media fayllarını stream edir.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MediaController(IStorageService storageService) : ControllerBase
{
    /// <summary>
    /// Verilmiş object key-ə uyğun faylı MinIO-dan qaytarır.
    /// </summary>
    [HttpGet("{**path}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string path, CancellationToken cancellationToken)
    {
        try
        {
            var stream = await storageService.GetAsync(path);
            var contentType = GetContentType(path);
            return File(stream, contentType);
        }
        catch
        {
            return NotFound();
        }
    }

    private static string GetContentType(string path)
    {
        var ext = Path.GetExtension(path).ToLowerInvariant();
        return ext switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".webp" => "image/webp",
            ".svg" => "image/svg+xml",
            ".pdf" => "application/pdf",
            _ => "application/octet-stream"
        };
    }
}
