using App.Core.Entities.Common.Cloudinary;

namespace App.Core.Entities;

public class Training : Event
{
    // EF Core materialization
    private Training() { }

    public Training(string titleAz, string titleEn, string titleRu, CloudinaryURL titleImageUrl, string textAz, string textEn, string textRu, DateTime startdate, DateTime enddate)
        : base(titleAz, titleEn, titleRu, titleImageUrl, textAz, textEn, textRu, startdate, enddate) { }
}