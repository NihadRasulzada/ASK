using App.Core.Entities.Common;
using App.Core.Entities.Common.Storage;

namespace App.Core.Entities;

public class Gallery : BaseEntity
{
    public StoredFile ImageUrl { get; private set; }

    private Gallery() : base(Guid.Empty)
    {
    }

    public Gallery(StoredFile imageUrl) : base(Guid.NewGuid())
    {
        if (StoredFile.IsNullOrEmpty(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));

        ImageUrl = imageUrl;
    }

    public void UpdateImageUrl(StoredFile imageUrl)
    {
        if (StoredFile.IsNullOrEmpty(imageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(imageUrl));

        ImageUrl = imageUrl;
    }
}
