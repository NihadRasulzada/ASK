namespace App.Core.Entities;

public class Partner : BaseEntity
{
    public string ImageUrl { get; private set; }
    public string Site { get; private set; }

    // EF Core materialization üçün private parameterless constructor
    private Partner()
    {
        ImageUrl = string.Empty;
        Site = string.Empty;
    }

    /// <summary>
    /// Yeni Partner yaradır.
    /// </summary>
    /// <param name="imageUrl">Partnerin şəkil URL-i (Cloudinary).</param>
    /// <param name="site">Partnerin sayt URL-i.</param>
    public Partner(string imageUrl, string site)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));

        if (string.IsNullOrWhiteSpace(site))
            throw new ArgumentException("Sayt URL-i boş ola bilməz.", nameof(site));

        ImageUrl = imageUrl;
        Site = site;
    }

    /// <summary>
    /// Mövcud Partnerin məlumatlarını yeniləyir.
    /// </summary>
    /// <param name="imageUrl">Yeni şəkil URL-i; null olarsa köhnə URL saxlanır.</param>
    /// <param name="site">Yeni sayt URL-i.</param>
    public void Update(string? imageUrl, string site)
    {
        if (string.IsNullOrWhiteSpace(site))
            throw new ArgumentException("Sayt URL-i boş ola bilməz.", nameof(site));

        if (imageUrl is not null)
            ImageUrl = imageUrl;

        Site = site;
    }
}
