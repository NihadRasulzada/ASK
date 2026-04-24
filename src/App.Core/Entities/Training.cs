using App.Core.Entities.Common.Cloudinary;

namespace App.Core.Entities;

public class Training : Event
{
    // EF Core materialization
    private Training() { }

    public Training(string title, CloudinaryURL titleImageUrl, string text)
        : base(title, titleImageUrl, text) { }
}