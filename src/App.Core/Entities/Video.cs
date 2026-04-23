using App.Core.Entities.Common;

namespace App.Core.Entities;

public class Video : BaseEntity
{
    public string Link { get; private set; }
    public string Title { get; private set; }

    // EF Core materialization üçün private parameterless constructor
    private Video() : base(Guid.Empty)
    {
    }

    /// <summary>
    /// Yeni Video yaradır.
    /// </summary>
    /// <param name="link">Videonun URL linki.</param>
    public Video(string link, string title) : base(Guid.NewGuid())
    {
        if (string.IsNullOrWhiteSpace(link))
            throw new ArgumentException("Link boş ola bilməz.", nameof(link));

        Link = link;
        Title = title;
    }

    /// <summary>
    /// Mövcud Video-nun linkini yeniləyir.
    /// </summary>
    /// <param name="link">Yeni URL link.</param>
    public void Update(string link, string title)
    {
        if (string.IsNullOrWhiteSpace(link))
            throw new ArgumentException("Link boş ola bilməz.", nameof(link));

        Link = link;
        Title = title;
    }
}