namespace App.BL.Services.External;

public interface IMediaUrlBuilder
{
    /// <summary>
    /// Storage object key və ya absolute URL-dən client üçün URL qurur.
    /// Nümunə: "http://host/api/media/images/2026/07/file.jpg"
    /// </summary>
    string? Build(string? cloudinaryUrlOrPath);

    string BuildHtml(string? html);
}
