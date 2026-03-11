using App.Core.Entities.Common;

namespace App.Core.Entities;

public class Service : SoftDeletableEntity
{
    //TODO: dil mentiqi
    public string ImageUrl { get; private set; }
    public string NameAz { get; private set; }
    public string NameEn { get; private set; }
    public string NameRu { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Service() : base(Guid.Empty, false)
    {
        ImageUrl = string.Empty;
        NameAz = string.Empty;
        NameEn = string.Empty;
        NameRu = string.Empty;
    }

    public Service(string imageUrl, string nameAz, string nameEn, string nameRu) : base(Guid.NewGuid(), false)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));

        if (string.IsNullOrWhiteSpace(nameAz))
            throw new ArgumentException("Az dili adı boş ola bilməz.", nameof(nameAz));

        if (string.IsNullOrWhiteSpace(nameEn))
            throw new ArgumentException("En dili adı boş ola bilməz.", nameof(nameEn));

        if (string.IsNullOrWhiteSpace(nameRu))
            throw new ArgumentException("Ru dili adı boş ola bilməz.", nameof(nameRu));

        ImageUrl = imageUrl;
        NameAz = nameAz;
        NameEn = nameEn;
        NameRu = nameRu;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string? imageUrl, string nameAz, string nameEn, string nameRu)
    {
        if (string.IsNullOrWhiteSpace(nameAz))
            throw new ArgumentException("Az dili adı boş ola bilməz.", nameof(nameAz));

        if (string.IsNullOrWhiteSpace(nameEn))
            throw new ArgumentException("En dili adı boş ola bilməz.", nameof(nameEn));

        if (string.IsNullOrWhiteSpace(nameRu))
            throw new ArgumentException("Ru dili adı boş ola bilməz.", nameof(nameRu));

        if (imageUrl is not null)
            ImageUrl = imageUrl;

        NameAz = nameAz;
        NameEn = nameEn;
        NameRu = nameRu;
    }
}
