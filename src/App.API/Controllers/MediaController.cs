using App.BL.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace App.API.Controllers;

/// <summary>
/// Cloudinary media fayllarını öz domain üzərindən proxy edir.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MediaController(
    IHttpClientFactory httpClientFactory,
    IOptions<CloudinarySettings> cloudinarySettings) : ControllerBase
{
    private readonly string _cloudName = cloudinarySettings.Value.CloudName;

    /// <summary>
    /// Cloudinary-dəki media faylını (şəkil, PDF və s.) proxy vasitəsilə qaytarır.
    /// URL-in öz domaininizdə qalması üçün istifadə edin.
    /// </summary>
    /// <param name="path">Cloudinary nisbi yolu (məs. image/upload/v123/file.jpg).</param>
    /// <param name="cancellationToken">Ləğvetmə tokeni.</param>
    /// <returns>Fayl axını.</returns>
    /// <response code="200">Fayl uğurla qaytarıldı.</response>
    /// <response code="404">Fayl tapılmadı.</response>
    [HttpGet("{**path}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string path, CancellationToken cancellationToken)
    {
        var cloudinaryUrl = $"https://res.cloudinary.com/{_cloudName}/{path}";

        var client = httpClientFactory.CreateClient("CloudinaryProxy");
        var response = await client.GetAsync(cloudinaryUrl, cancellationToken);

        if (!response.IsSuccessStatusCode)
            return NotFound();

        var contentType = response.Content.Headers.ContentType?.ToString()
                          ?? "application/octet-stream";

        var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        return File(stream, contentType);
    }
}
