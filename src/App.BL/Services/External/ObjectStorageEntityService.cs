using App.Core.Entities.Common.Cloudinary;
using Microsoft.AspNetCore.Http;

namespace App.BL.Services.External;

public abstract class ObjectStorageEntityService(IObjectStorageService objectStorageService)
{
    protected async Task<(CloudinaryURL newUrl, string oldObjectName)> ReplaceImageAsync(
        string oldObjectName,
        IFormFile newFile)
    {
        CloudinaryURL newUrl = await objectStorageService.UploadImageAsync(newFile);
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
