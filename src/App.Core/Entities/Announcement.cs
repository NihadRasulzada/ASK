using App.Core.Entities.Common;
using App.Core.Entities.Common.Cloudinary;

namespace App.Core.Entities;

public class Announcement : BaseEntity
{
    public CloudinaryURL TitleImageUrl { get; private set; }
    public string Title { get; private set; }
    public string Text { get; private set; }

    public Guid CloudinaryURLId { get; private set; }

    public Announcement(string title, CloudinaryURL titleImageUrl, string text)
        : base(Guid.NewGuid())
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Başlıq boş ola bilməz.", nameof(title));

        if (CloudinaryURL.IsNullOrEmpty(titleImageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(titleImageUrl));

        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Mətn boş ola bilməz.", nameof(text));

        Title = title;
        TitleImageUrl = titleImageUrl;
        Text = text;
    }

    // EF Core materialization
    private Announcement() : base(Guid.Empty)
    {
        Title = string.Empty;
        Text = string.Empty;
    }

    /// <param name="titleImageUrl">Null ötürülərsə mövcud dəyər saxlanılır.</param>
    public void Update(string title, string text)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Başlıq boş ola bilməz.", nameof(title));

        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Mətn boş ola bilməz.", nameof(text));

        Title = title;
        Text = text;
    }

    public void UpdateImageUrl(CloudinaryURL titleImageUrl)
    {
        if (CloudinaryURL.IsNullOrEmpty(titleImageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(titleImageUrl));

        TitleImageUrl = titleImageUrl;
    }

}