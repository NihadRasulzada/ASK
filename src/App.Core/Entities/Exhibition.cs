using App.Core.Entities.Common.Cloudinary;

namespace App.Core.Entities;

public class Exhibition : Event
{
    // EF Core materialization
    private Exhibition() { }

    public Exhibition(string titleAz,string titleEn,string titleRu, CloudinaryURL titleImageUrl, string textAz,string textEn,string textRu)
        : base(titleAz, titleEn, titleRu, titleImageUrl, textAz, textEn, textRu) { }
}