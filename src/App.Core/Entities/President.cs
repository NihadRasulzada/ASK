using App.Core.Entities.Common;
using App.Core.Entities.Common.Storage;

namespace App.Core.Entities;

public class President : BaseEntity
{
    public StoredFile ImageUrl { get; private set; }
    public string Text { get; private set; }

    private President() : base(Guid.Empty)
    {
    }

    public President(StoredFile imageUrl, string text) : base(Guid.NewGuid())
    {
        if (StoredFile.IsNullOrEmpty(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Mətn boş ola bilməz.", nameof(text));

        ImageUrl = imageUrl;
        Text = text;
    }

    public void Update(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Mətn boş ola bilməz.", nameof(text));

        Text = text;
    }

    public void UpdateImageUrl(StoredFile imageUrl)
    {
        if (StoredFile.IsNullOrEmpty(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));

        ImageUrl = imageUrl;
    }
}
