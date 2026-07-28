using App.Core.Entities.Common.Storage;
using Microsoft.AspNetCore.Http;

namespace App.BL.Services.External;

public interface IObjectStorageService
{
    Task<StoredFile> UploadImageAsync(IFormFile file);
    Task<IList<StoredFile>> UploadImagesAsync(IEnumerable<IFormFile> files);
    Task<StoredFile> UploadPdfAsync(IFormFile file);
    Task<StoredFile> UploadAsync(Stream stream, string objectName, string contentType, long size, CancellationToken cancellationToken = default);
    Task DeleteAsync(string objectName);
    Task<ObjectStorageFile?> GetAsync(string objectName, CancellationToken cancellationToken = default);
}

public sealed record ObjectStorageFile(Stream Stream, string ContentType);
