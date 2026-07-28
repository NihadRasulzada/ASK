using App.BL.Services.External;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MediaController(IObjectStorageService objectStorageService) : ControllerBase
{
    [HttpGet("{**path}")]
    [ResponseCache(Duration = 604800, Location = ResponseCacheLocation.Any)]
    public async Task<IActionResult> Get(string path, CancellationToken cancellationToken)
    {
        var file = await objectStorageService.GetAsync(path, cancellationToken);
        if (file is null)
            return NotFound();

        return File(file.Stream, file.ContentType);
    }
}
