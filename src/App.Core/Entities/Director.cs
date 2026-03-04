namespace App.Core.Entities;

public class Director : BaseEntity
{
    public string ImageUrl { get; private set; }
    public string FullName { get; private set; }
    public string Duty { get; private set; }

    // EF Core materialization üçün private parameterless constructor
    private Director()
    {
        ImageUrl = string.Empty;
        FullName = string.Empty;
        Duty = string.Empty;
    }

    /// <summary>
    /// Yeni Direktor yaradır.
    /// </summary>
    /// <param name="imageUrl">Cloudinary şəkil URL-i.</param>
    /// <param name="fullName">Direktorun tam adı.</param>
    /// <param name="duty">Direktorun vəzifəsi.</param>
    public Director(string imageUrl, string fullName, string duty)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));

        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Tam ad boş ola bilməz.", nameof(fullName));

        if (string.IsNullOrWhiteSpace(duty))
            throw new ArgumentException("Vəzifə boş ola bilməz.", nameof(duty));

        ImageUrl = imageUrl;
        FullName = fullName;
        Duty = duty;
    }

    /// <summary>
    /// Mövcud Direktorun məlumatlarını yeniləyir.
    /// </summary>
    /// <param name="imageUrl">Yeni şəkil URL-i; null olarsa köhnə URL saxlanır.</param>
    /// <param name="fullName">Yeni tam ad.</param>
    /// <param name="duty">Yeni vəzifə.</param>
    public void Update(string? imageUrl, string fullName, string duty)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Tam ad boş ola bilməz.", nameof(fullName));

        if (string.IsNullOrWhiteSpace(duty))
            throw new ArgumentException("Vəzifə boş ola bilməz.", nameof(duty));

        if (imageUrl is not null)
            ImageUrl = imageUrl;

        FullName = fullName;
        Duty = duty;
    }
}
