using App.Core.Entities.Common;

namespace App.Core.Entities;

public class UsefulLink : SoftDeletableEntity
{
    public string TitleAz { get; private set; }
    public string TitleEn { get; private set; }
    public string TitleRu { get; private set; }
    public string Link { get; private set; }

    private UsefulLink() : base(Guid.Empty, false)
    {
    }

    public UsefulLink(string titleAz, string titleEn, string titleRu, string link)
        : base(Guid.NewGuid(), false)
    {
        TitleAz = titleAz;
        TitleEn = titleEn;
        TitleRu = titleRu;
        Link = link;
    }

    public void Update(string titleAz, string titleEn, string titleRu, string link)
    {
        TitleAz = titleAz;
        TitleEn = titleEn;
        TitleRu = titleRu;
        Link = link;
    }
}
