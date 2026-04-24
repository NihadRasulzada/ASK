
using App.Core.Entities.Common.Cloudinary;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace App.BL.Services.External;


public abstract class CloudinaryEntityService(ICloudinaryService cloudinaryService)
{
    /// <summary>
    /// Mövcud şəkili silir, yenisini yükləyir, köhnə publicId qaytarır.
    /// Upload uğursuz olsa köhnə şəkil toxunulmaz qalır.
    /// </summary>
    protected async Task<(CloudinaryURL newUrl, string oldPublicId)> ReplaceImageAsync(
        string oldPublicId,
        IFormFile newFile)
    {
        // Əvvəl yüklə, sonra sil — data itkisinin qarşısını alır
        CloudinaryURL newUrl = await cloudinaryService.UploadImageAsync(newFile);
        return (newUrl, oldPublicId);
    }

    /// <summary>
    /// Köhnə şəkili Cloudinary-dən silir. PublicId boşdursa skip edir.
    /// </summary>
    protected async Task DeleteImageAsync(string? publicId, ResourceType resourceType = ResourceType.Image)
    {
        if (string.IsNullOrWhiteSpace(publicId)) return;
        await cloudinaryService.DeleteAsync(publicId, resourceType);
    }
}