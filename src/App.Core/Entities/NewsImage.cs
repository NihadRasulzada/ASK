using App.Core.Entities.Common;

namespace App.Core.Entities;

public class NewsImage : BaseEntity
{
    public string ImageUrl { get; private set; }

    public News News { get; private set; }
    public Guid NewsId { get; private set; }

    public NewsImage(string imageUrl, Guid newsId) : base(Guid.NewGuid())
    {
        ImageUrl = imageUrl;
        NewsId = newsId;
    }

    private NewsImage() : base(Guid.Empty)
    {
    }

    public void Update(string imageUrl)
    {
        ImageUrl = imageUrl;
    }
}