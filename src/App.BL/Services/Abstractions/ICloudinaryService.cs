using Microsoft.AspNetCore.Http;

namespace App.BL.Services.Abstractions;

public interface ICloudinaryService
{
    /// <summary>
    /// Şəkili Cloudinary-yə yükləyir və təhlükəsiz URL qaytarır.
    /// </summary>
    /// <param name="file">Yüklənəcək şəkil faylı.</param>
    /// <returns>Cloudinary-dəki şəkilin HTTPS URL-i.</returns>
    Task<string> UploadImageAsync(IFormFile file);
}
