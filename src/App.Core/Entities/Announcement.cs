using App.Core.Entities.Common;

namespace App.Core.Entities;

public class Announcement : BaseEntity
{
    public string TitleImageUrl { get; private set; }
    public string Title { get; private set; }
    public string Text { get; private set; }

    public Announcement(string title, string titleImageUrl, string text)
        : base(Guid.NewGuid())
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Başlıq boş ola bilməz.", nameof(title));

        if (string.IsNullOrWhiteSpace(titleImageUrl))
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
        TitleImageUrl = string.Empty;
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

    public void UpdateImage(string titleImageUrl)
    {
        if (string.IsNullOrWhiteSpace(titleImageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(titleImageUrl));

        TitleImageUrl = titleImageUrl;
    }
}