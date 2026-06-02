using App.Core.Entities.Common.Storage;
using Microsoft.AspNetCore.Http;

namespace App.BL.Services.External;

public abstract class StorageEntityService(IStorageService storageService)
{
    protected async Task<(StoredFile newFile, string oldObjectKey)> ReplaceFileAsync(
        string oldObjectKey, IFormFile newFile, string? folder = null)
    {
        StoredFile newStored = await storageService.UploadAsync(newFile, folder);
        return (newStored, oldObjectKey);
    }

    protected async Task DeleteFileAsync(string? objectKey)
    {
        if (string.IsNullOrWhiteSpace(objectKey)) return;
        await storageService.DeleteAsync(objectKey);
    }
}
