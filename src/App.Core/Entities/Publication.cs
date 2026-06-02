using App.Core.Entities.Common;
using App.Core.Entities.Common.Storage;

namespace App.Core.Entities;

public class Publication : BaseEntity
{
    public StoredFile TitleImageUrl { get; private set; }
    public string TitleAz { get; private set; }
    public string TitleEn { get; private set; }
    public string TitleRu { get; private set; }
    public StoredFile PdfUrl { get; private set; }

    private Publication() : base(Guid.Empty) { }

    public Publication(StoredFile titleImageUrl, string titleAz, string titleEn, string titleRu, StoredFile pdfUrl)
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

    public void UpdateTitleImage(StoredFile url) => TitleImageUrl = url;
    public void UpdatePdf(StoredFile url) => PdfUrl = url;
}
