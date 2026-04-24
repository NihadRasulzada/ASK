using App.Core.Entities.Common;
using App.Core.Entities.Common.Cloudinary;

namespace App.Core.Entities;

// FIX: abstract olmalıdır — heç vaxt birbaşa "Event" yaradılmır,
//      yalnız Exhibition / Training yaradılır.
// FIX: setter-lər private olmalıdır.
// FIX: EF Core üçün parameterless constructor əlavə edildi.
public abstract class Event : SoftDeletableEntity
{
    public string Title { get; private set; }
    public CloudinaryURL TitleImageUrl { get; private set; }
    public string Text { get; private set; }

    public Guid CloudinaryURLId { get; private set; }

    public DateTime Created { get; private set; } = DateTime.UtcNow;

    // EF Core materialization
    protected Event() : base(Guid.Empty, false)
    {
    }

    protected Event(string title, CloudinaryURL titleImageUrl, string text)
        : base(Guid.NewGuid(), false)
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