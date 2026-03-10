using App.Core.Entities.Common;

namespace App.Core.Entities;

public class Service : SoftDeletableEntity
{
    public string ImageUrl { get; private set; }
    public string Name { get; private set; }

    public Service(string name, string imageUrl) : base(Guid.NewGuid(), false)
    {
        Name = name;
        ImageUrl = imageUrl;
    }
}