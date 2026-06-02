using App.Core.Entities.Common.Storage;
using Microsoft.AspNetCore.Http;

namespace App.BL.Services.External;

public interface IStorageService
{
    Task<StoredFile> UploadAsync(IFormFile file, string? folder = null);
    Task<IList<StoredFile>> UploadManyAsync(IEnumerable<IFormFile> files, string? folder = null);
    Task DeleteAsync(string objectKey);
    Task<Stream> GetAsync(string objectKey);
}
