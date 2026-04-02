namespace App.BL.Services.External;

public interface IMediaUrlBuilder
{
    /// <summary>
    /// Cloudinary tam URL və ya relative path-dən tam proxy URL qurur.
    /// Nümunə: "http://host/api/media/image/upload/v123/file.jpg"
    /// </summary>
    string? Build(string? cloudinaryUrlOrPath);
}
