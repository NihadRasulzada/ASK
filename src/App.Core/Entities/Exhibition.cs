using App.Core.Entities.Common.Storage;

namespace App.Core.Entities;

public class Exhibition : Event
{
    private Exhibition() { }

    public Exhibition(string titleAz, string titleEn, string titleRu, StoredFile titleImageUrl, string textAz, string textEn, string textRu, DateTime startdate, DateTime enddate)
        : base(titleAz, titleEn, titleRu, titleImageUrl, textAz, textEn, textRu, startdate, enddate) { }
}
