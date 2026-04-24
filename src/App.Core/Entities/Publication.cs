using App.Core.Entities.Common;
using App.Core.Entities.Common.Cloudinary;

namespace App.Core.Entities;

public class Publication : BaseEntity
{
    public CloudinaryURL TitleImageUrl { get; private set; }
    public string TitleAz { get; private set; }
    public string TitleEn { get; private set; }
    public string TitleRu { get; private set; }
    public CloudinaryURL PdfUrl { get; private set; }

    public Guid CloudinaryURLId { get; private set; }


    private Publication() : base(Guid.Empty) { }

    public Publication(CloudinaryURL titleImageUrl, string titleAz, string titleEn, string titleRu, CloudinaryURL pdfUrl)
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

    public void UpdateTitleImage(CloudinaryURL url) => TitleImageUrl = url;
    public void UpdatePdf(CloudinaryURL url) => PdfUrl = url;
}
