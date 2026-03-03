namespace App.Core.Entities;

public class President : BaseEntity
{
    public string ImageUrl { get; private set; }
    public string Text { get; private set; }

    // EF Core materialization üçün private parameterless constructor
    private President()
    {
        ImageUrl = string.Empty;
        Text = string.Empty;
    }

    /// <summary>
    /// Yeni Prezident məlumatı yaradır.
    /// </summary>
    /// <param name="imageUrl">Cloudinary şəkil URL-i.</param>
    /// <param name="text">Prezident haqqında mətn.</param>
    public President(string imageUrl, string text)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));

        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Mətn boş ola bilməz.", nameof(text));

        ImageUrl = imageUrl;
        Text = text;
    }

    /// <summary>
    /// Mövcud Prezident məlumatını yeniləyir.
    /// </summary>
    /// <param name="imageUrl">Yeni şəkil URL-i; null olarsa köhnə URL saxlanır.</param>
    /// <param name="text">Yeni mətn.</param>
    public void Update(string? imageUrl, string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Mətn boş ola bilməz.", nameof(text));

        if (imageUrl is not null)
            ImageUrl = imageUrl;

        Text = text;
    }
}
