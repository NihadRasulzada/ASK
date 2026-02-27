namespace App.Core.Entities;

public class News : AuditableEntity
{
    public string TitleImageUrl { get; private set; }
    public string NewsText { get; private set; }
    public IList<string> ImageUrls { get; private set; }

    // EF Core materialization üçün private parameterless constructor
    private News()
    {
        TitleImageUrl = string.Empty;
        NewsText = string.Empty;
        ImageUrls = new List<string>();
    }

    /// <summary>
    /// Yeni Xəbər yaradır.
    /// </summary>
    /// <param name="titleImageUrl">Başlıq şəklinin Cloudinary URL-i.</param>
    /// <param name="newsText">Xəbərin mətni.</param>
    /// <param name="imageUrls">Əlavə şəkillərin URL siyahısı.</param>
    public News(string titleImageUrl, string newsText, IList<string> imageUrls)
    {
        if (string.IsNullOrWhiteSpace(titleImageUrl))
            throw new ArgumentException("Başlıq şəkli URL-i boş ola bilməz.", nameof(titleImageUrl));

        if (string.IsNullOrWhiteSpace(newsText))
            throw new ArgumentException("Xəbər mətni boş ola bilməz.", nameof(newsText));

        TitleImageUrl = titleImageUrl;
        NewsText = newsText;
        ImageUrls = imageUrls ?? new List<string>();
    }

    /// <summary>
    /// Mövcud Xəbərin məlumatlarını yeniləyir.
    /// </summary>
    /// <param name="titleImageUrl">Yeni başlıq şəkli URL-i; null olarsa köhnə URL saxlanır.</param>
    /// <param name="newsText">Yeni xəbər mətni.</param>
    /// <param name="imageUrls">Yeni əlavə şəkil URL siyahısı; null olarsa köhnə siyahı saxlanır.</param>
    public void Update(string? titleImageUrl, string newsText, IList<string>? imageUrls)
    {
        if (string.IsNullOrWhiteSpace(newsText))
            throw new ArgumentException("Xəbər mətni boş ola bilməz.", nameof(newsText));

        if (titleImageUrl is not null)
            TitleImageUrl = titleImageUrl;

        NewsText = newsText;

        if (imageUrls is not null)
            ImageUrls = imageUrls;
    }
}
