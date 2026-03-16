using App.Core.Entities.Common;

namespace App.Core.Entities;

public class President : BaseEntity
{
    public string ImageUrl { get; private set; }
    public string Text { get; private set; }

    private President() : base(Guid.Empty)
    {
        ImageUrl = string.Empty;
        Text = string.Empty;
    }

    public President(string imageUrl, string text) : base(Guid.NewGuid())
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
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

    public void UpdateImageUrl(string imageUrl)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));

        ImageUrl = imageUrl;
    }
}