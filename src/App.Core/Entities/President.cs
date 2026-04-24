using App.Core.Entities.Common;
using App.Core.Entities.Common.Cloudinary;

namespace App.Core.Entities;

public class President : BaseEntity
{
    public CloudinaryURL ImageUrl { get; private set; }
    public string Text { get; private set; }

    public Guid CloudinaryURLId { get; private set; }

    private President() : base(Guid.Empty)
    {
    }

    public President(CloudinaryURL imageUrl, string text) : base(Guid.NewGuid())
    {
        if (CloudinaryURL.IsNullOrEmpty(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Mətn boş ola bilməz.", nameof(text));

        ImageUrl = imageUrl;
        Text = text;
    }

    /// <param name="imageUrl">Null ötürülərsə mövcud dəyər saxlanılır.</param>
    public void Update(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Mətn boş ola bilməz.", nameof(text));

        Text = text;
    }

    public void UpdateImageUrl(CloudinaryURL imageUrl)
    {
        if (CloudinaryURL.IsNullOrEmpty(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));

        ImageUrl = imageUrl;
    }
}