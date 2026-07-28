using App.BL.Settings;
using App.Core.Entities.Common.Cloudinary;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace App.BL.Services.External;

public class MinioObjectStorageService : IObjectStorageService
{
    private readonly IMinioClient client;
    private readonly MinioSettings settings;
    private bool bucketChecked;

    public MinioObjectStorageService(IOptions<MinioSettings> options)
    {
        settings = options.Value;
        client = new MinioClient()
            .WithEndpoint(settings.Endpoint)
            .WithCredentials(settings.AccessKey, settings.SecretKey)
            .WithSSL(settings.UseSsl)
            .Build();
    }

    public async Task<CloudinaryURL> UploadImageAsync(IFormFile file)
    {
        await EnsureBucketAsync();
        var objectName = BuildObjectName("images", file.FileName);
        await PutAsync(file, objectName);
        return new CloudinaryURL(objectName, objectName);
    }

    public async Task<IList<CloudinaryURL>> UploadImagesAsync(IEnumerable<IFormFile> files)
    {
        var result = new List<CloudinaryURL>();
        foreach (var file in files)
            result.Add(await UploadImageAsync(file));

        return result;
    }

    public async Task<CloudinaryURL> UploadPdfAsync(IFormFile file)
    {
        const string pdfContentType = "application/pdf";
        if (!string.Equals(file.ContentType, pdfContentType, StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Yalnız PDF fayl qəbul edilir (application/pdf).");

        await EnsureBucketAsync();
        var objectName = BuildObjectName("documents", file.FileName);
        await PutAsync(file, objectName);
        return new CloudinaryURL(objectName, objectName);
    }

    public async Task DeleteAsync(string objectName)
    {
        if (string.IsNullOrWhiteSpace(objectName)) return;
        if (objectName.StartsWith("wordpress/", StringComparison.OrdinalIgnoreCase)) return;
        if (objectName.StartsWith("wordpress-seed/", StringComparison.OrdinalIgnoreCase)) return;
        if (objectName.StartsWith("seed-media/", StringComparison.OrdinalIgnoreCase)) return;

        await EnsureBucketAsync();
        try
        {
            await client.RemoveObjectAsync(new RemoveObjectArgs()
                .WithBucket(settings.BucketName)
                .WithObject(objectName));
        }
        catch (ObjectNotFoundException)
        {
        }
    }

    public async Task<CloudinaryURL> UploadAsync(Stream stream, string objectName, string contentType, long size, CancellationToken cancellationToken = default)
    {
        await EnsureBucketAsync();
        await client.PutObjectAsync(new PutObjectArgs()
            .WithBucket(settings.BucketName)
            .WithObject(objectName)
            .WithStreamData(stream)
            .WithObjectSize(size)
            .WithContentType(contentType), cancellationToken);

        return new CloudinaryURL(objectName, objectName);
    }

    public async Task<ObjectStorageFile?> GetAsync(string objectName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(objectName))
            return null;

        await EnsureBucketAsync();
        var stream = new MemoryStream();
        var contentType = "application/octet-stream";

        try
        {
            await client.GetObjectAsync(new GetObjectArgs()
                .WithBucket(settings.BucketName)
                .WithObject(objectName)
                .WithCallbackStream(source =>
                {
                    source.CopyTo(stream);
                }), cancellationToken);

            stream.Position = 0;
            contentType = GuessContentType(objectName);
            return new ObjectStorageFile(stream, contentType);
        }
        catch (ObjectNotFoundException)
        {
            await stream.DisposeAsync();
            return null;
        }
    }

    private async Task PutAsync(IFormFile file, string objectName)
    {
        await using var stream = file.OpenReadStream();
        await UploadAsync(stream, objectName, file.ContentType, file.Length);
    }

    private async Task EnsureBucketAsync()
    {
        if (bucketChecked) return;

        var exists = await client.BucketExistsAsync(new BucketExistsArgs().WithBucket(settings.BucketName));
        if (!exists)
            await client.MakeBucketAsync(new MakeBucketArgs().WithBucket(settings.BucketName));

        bucketChecked = true;
    }

    private static string BuildObjectName(string folder, string fileName)
    {
        var extension = Path.GetExtension(fileName);
        var safeExtension = string.IsNullOrWhiteSpace(extension) ? ".bin" : extension.ToLowerInvariant();
        return $"{folder}/{DateTime.UtcNow:yyyy/MM}/{Guid.NewGuid():N}{safeExtension}";
    }

    private static string GuessContentType(string objectName)
    {
        var extension = Path.GetExtension(objectName).ToLowerInvariant();
        return extension switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".webp" => "image/webp",
            ".svg" => "image/svg+xml",
            ".pdf" => "application/pdf",
            _ => "application/octet-stream"
        };
    }
}
