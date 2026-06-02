using App.BL.Settings;
using App.Core.Entities.Common.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;

namespace App.BL.Services.External;

public class MinioService(IMinioClient minioClient, IOptions<MinioSettings> settings) : IStorageService
{
    private readonly string _bucket = settings.Value.BucketName;

    public async Task<StoredFile> UploadAsync(IFormFile file, string? folder = null)
    {
        var ext = Path.GetExtension(file.FileName);
        var objectKey = string.IsNullOrWhiteSpace(folder)
            ? $"{Guid.NewGuid()}{ext}"
            : $"{folder.TrimEnd('/')}/{Guid.NewGuid()}{ext}";

        await using var stream = file.OpenReadStream();
        var args = new PutObjectArgs()
            .WithBucket(_bucket)
            .WithObject(objectKey)
            .WithStreamData(stream)
            .WithObjectSize(file.Length)
            .WithContentType(file.ContentType);

        await minioClient.PutObjectAsync(args);
        return new StoredFile(objectKey);
    }

    public async Task<IList<StoredFile>> UploadManyAsync(IEnumerable<IFormFile> files, string? folder = null)
    {
        var results = new List<StoredFile>();
        foreach (var file in files)
            results.Add(await UploadAsync(file, folder));
        return results;
    }

    public async Task DeleteAsync(string objectKey)
    {
        if (string.IsNullOrWhiteSpace(objectKey)) return;
        var args = new RemoveObjectArgs()
            .WithBucket(_bucket)
            .WithObject(objectKey);
        await minioClient.RemoveObjectAsync(args);
    }

    public async Task<Stream> GetAsync(string objectKey)
    {
        var ms = new MemoryStream();
        var args = new GetObjectArgs()
            .WithBucket(_bucket)
            .WithObject(objectKey)
            .WithCallbackStream(stream => stream.CopyTo(ms));
        await minioClient.GetObjectAsync(args);
        ms.Position = 0;
        return ms;
    }
}
