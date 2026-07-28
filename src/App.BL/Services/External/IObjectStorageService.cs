using App.Core.Entities.Common.Cloudinary;
using Microsoft.AspNetCore.Http;

namespace App.BL.Services.External;

public interface IObjectStorageService
{
    Task<CloudinaryURL> UploadImageAsync(IFormFile file);
    Task<IList<CloudinaryURL>> UploadImagesAsync(IEnumerable<IFormFile> files);
    Task<CloudinaryURL> UploadPdfAsync(IFormFile file);
    Task<CloudinaryURL> UploadAsync(Stream stream, string objectName, string contentType, long size, CancellationToken cancellationToken = default);
    Task DeleteAsync(string objectName);
    Task<ObjectStorageFile?> GetAsync(string objectName, CancellationToken cancellationToken = default);
}

public sealed record ObjectStorageFile(Stream Stream, string ContentType);
