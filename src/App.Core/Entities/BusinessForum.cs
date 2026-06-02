using App.Core.Entities.Common.Storage;

namespace App.Core.Entities;

public class BusinessForum : Event
{
    public StoredFile DetailImageUrl { get; private set; }

    public BusinessForum() { }

    public BusinessForum(string titleAz, string titleEn, string titleRu, StoredFile titleImageUrl, string textAz, string textEn, string textRu, DateTime startdate, DateTime enddate, StoredFile detailImageUrl)
        : base(titleAz, titleEn, titleRu, titleImageUrl, textAz, textEn, textRu, startdate, enddate)
    {
        DetailImageUrl = detailImageUrl;
    }

    public void UpdateDetailImageUrl(StoredFile detailImageUrl)
    {
        if (StoredFile.IsNullOrEmpty(detailImageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(detailImageUrl));

        DetailImageUrl = detailImageUrl;
    }
}
