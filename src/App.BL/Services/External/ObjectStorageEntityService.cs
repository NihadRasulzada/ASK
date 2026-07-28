using App.Core.Entities.Common.Storage;
using Microsoft.AspNetCore.Http;

namespace App.BL.Services.External;

public abstract class ObjectStorageEntityService(IObjectStorageService objectStorageService)
{
    protected async Task<(StoredFile newUrl, string oldObjectName)> ReplaceImageAsync(
        string oldObjectName,
        IFormFile newFile)
    {
        StoredFile newUrl = await objectStorageService.UploadImageAsync(newFile);
        return (newUrl, oldObjectName);
    }

    protected async Task<(StoredFile newUrl, string oldObjectName)> ReplacePdfAsync(
        string oldObjectName,
        IFormFile newFile)
    {
        StoredFile newUrl = await objectStorageService.UploadPdfAsync(newFile);
        return (newUrl, oldObjectName);
    }

    protected async Task DeleteImageAsync(string? objectName)
    {
        if (string.IsNullOrWhiteSpace(objectName)) return;
        if (objectName.StartsWith("wordpress/", StringComparison.OrdinalIgnoreCase)) return;
        if (objectName.StartsWith("wordpress-seed/", StringComparison.OrdinalIgnoreCase)) return;
        if (objectName.StartsWith("seed-media/", StringComparison.OrdinalIgnoreCase)) return;

        await objectStorageService.DeleteAsync(objectName);
    }
}
