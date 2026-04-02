using App.BL.Settings;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace App.BL.Services.External;

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinary;
    private readonly string _cloudName;

    public CloudinaryService(IOptions<CloudinarySettings> settings)
    {
        var s = settings.Value;
        _cloudName = s.CloudName;
        var account = new Account(s.CloudName, s.ApiKey, s.ApiSecret);
        _cloudinary = new Cloudinary(account) { Api = { Secure = true } };
    }

    // Returns the path portion after the cloud name, e.g. "image/upload/v123/file.jpg"
    private string ToRelativePath(Uri secureUrl)
    {
        var path = secureUrl.AbsolutePath.TrimStart('/');
        var prefix = _cloudName + "/";
        return path.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)
            ? path[prefix.Length..]
            : path;
    }

    /// <inheritdoc/>
    public async Task<string> UploadImageAsync(IFormFile file)
    {
        await using var stream = file.OpenReadStream();

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            UseFilename = true,
            UniqueFilename = true,
            Overwrite = false,
            Transformation = new Transformation().Quality("auto:best")
        };

        var result = await _cloudinary.UploadAsync(uploadParams);

        if (result.Error is not null)
            throw new InvalidOperationException($"Cloudinary yükləmə xətası: {result.Error.Message}");

        return ToRelativePath(result.SecureUrl);
    }

    /// <inheritdoc/>
    public async Task<IList<string>> UploadImagesAsync(IEnumerable<IFormFile> files)
    {
        var urls = new List<string>();

        foreach (var file in files)
        {
            var url = await UploadImageAsync(file);
            urls.Add(url);
        }

        return urls;
    }

    /// <inheritdoc/>
    public async Task<string> UploadPdfAsync(IFormFile file)
    {
        const string pdfContentType = "application/pdf";
        const long maxSizeBytes = 10L * 1024 * 1024; // 10 MB

        if (!string.Equals(file.ContentType, pdfContentType, StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Yalnız PDF fayl qəbul edilir (application/pdf).");

        if (file.Length > maxSizeBytes)
            throw new InvalidOperationException("PDF faylının ölçüsü 10 MB-dan çox ola bilməz.");

        await using var stream = file.OpenReadStream();

        var uploadParams = new RawUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            UseFilename = true,
            UniqueFilename = true,
            Overwrite = false
        };

        var result = await _cloudinary.UploadAsync(uploadParams);

        if (result.Error is not null)
            throw new InvalidOperationException($"Cloudinary yükləmə xətası: {result.Error.Message}");

        return ToRelativePath(result.SecureUrl);
    }
}
