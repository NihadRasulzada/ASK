using App.Core.Entities.Common;
using App.Core.Entities.Common.Cloudinary;

namespace App.Core.Entities;

public class BusinessForum : BaseEntity
{
    public CloudinaryURL TitleImageUrl { get; private set; }
    public string TitleAz { get; private set; }
    public string TitleEn { get; private set; }
    public string TitleRu { get; private set; }
    public DateTime CreateDate { get; private set; }
    public string TextAz { get; private set; }
    public string TextEn { get; private set; }
    public string TextRu { get; private set; }
    public CloudinaryURL DetailImageUrl { get; private set; }


    public Guid CloudinaryURLId { get; private set; }

    private BusinessForum() : base(Guid.Empty) { }

    public BusinessForum(CloudinaryURL titleImageUrl, string titleAz, string titleEn, string titleRu,
                         string textAz, string textEn, string textRu, CloudinaryURL detailImageUrl)
        : base(Guid.NewGuid())
    {
        TitleImageUrl = titleImageUrl;
        TitleAz = titleAz;
        TitleEn = titleEn;
        TitleRu = titleRu;
        TextAz = textAz;
        TextEn = textEn;
        TextRu = textRu;
        DetailImageUrl = detailImageUrl;
        CreateDate = DateTime.UtcNow;
    }

    public void Update(string titleAz, string titleEn, string titleRu,
                       string textAz, string textEn, string textRu)
    {
        TitleAz = titleAz;
        TitleEn = titleEn;
        TitleRu = titleRu;
        TextAz = textAz;
        TextEn = textEn;
        TextRu = textRu;
    }
    public void UpdateTitleImageUrl(CloudinaryURL titleImageUrl)
    {
        if (CloudinaryURL.IsNullOrEmpty(titleImageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(titleImageUrl));

        TitleImageUrl = titleImageUrl;
    }

    public void UpdateDetailImageUrl(CloudinaryURL detailImageUrl)
    {
        if (CloudinaryURL.IsNullOrEmpty(detailImageUrl))
            throw new ArgumentException("Şəkil URL-i boş ola bilməz.", nameof(detailImageUrl));

        DetailImageUrl = detailImageUrl;
    }

}
