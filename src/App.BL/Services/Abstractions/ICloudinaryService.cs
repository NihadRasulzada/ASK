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

    /// <summary>
    /// Bir neçə şəkili Cloudinary-yə yükləyir və URL siyahısı qaytarır.
    /// </summary>
    /// <param name="files">Yüklənəcək şəkil faylları.</param>
    /// <returns>Cloudinary-dəki şəkillərin HTTPS URL siyahısı.</returns>
    Task<IList<string>> UploadImagesAsync(IEnumerable<IFormFile> files);
}
