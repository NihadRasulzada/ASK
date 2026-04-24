using App.Core.Entities.Common.Cloudinary;

namespace App.Core.Entities;

public class Exhibition : Event
{
    // EF Core materialization
    private Exhibition() { }

    public Exhibition(string title, CloudinaryURL titleImageUrl, string text)
        : base(title, titleImageUrl, text) { }
}