using App.Core.Entities.Common;

namespace App.Core.Entities;

public class BusinessForum : BaseEntity
{
    public string TitleImageUrl { get; private set; }
    public string TitleAz { get; private set; }
    public string TitleEn { get; private set; }
    public string TitleRu { get; private set; }
    public DateTime CreateDate { get; private set; }
    public string TextAz { get; private set; }
    public string TextEn { get; private set; }
    public string TextRu { get; private set; }
    public string DetailImageUrl { get; private set; }

    private BusinessForum() : base(Guid.Empty) { }

    public BusinessForum(string titleImageUrl, string titleAz, string titleEn, string titleRu,
                         string textAz, string textEn, string textRu, string detailImageUrl)
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

    public void UpdateTitleImage(string url) => TitleImageUrl = url;
    public void UpdateDetailImage(string url) => DetailImageUrl = url;
}
