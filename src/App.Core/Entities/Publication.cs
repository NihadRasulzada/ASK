using App.Core.Entities.Common;

namespace App.Core.Entities;

public class Publication : BaseEntity
{
    public string TitleImageUrl { get; private set; }
    public string TitleAz { get; private set; }
    public string TitleEn { get; private set; }
    public string TitleRu { get; private set; }
    public string PdfUrl { get; private set; }

    private Publication() : base(Guid.Empty) { }

    public Publication(string titleImageUrl, string titleAz, string titleEn, string titleRu, string pdfUrl)
        : base(Guid.NewGuid())
    {
        TitleImageUrl = titleImageUrl;
        TitleAz = titleAz;
        TitleEn = titleEn;
        TitleRu = titleRu;
        PdfUrl = pdfUrl;
    }

    public void Update(string titleAz, string titleEn, string titleRu)
    {
        TitleAz = titleAz;
        TitleEn = titleEn;
        TitleRu = titleRu;
    }

    public void UpdateTitleImage(string url) => TitleImageUrl = url;
    public void UpdatePdf(string url) => PdfUrl = url;
}
