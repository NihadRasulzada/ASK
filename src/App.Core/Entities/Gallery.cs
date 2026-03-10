using App.Core.Entities.Common;

namespace App.Core.Entities
{
    public class Gallery() : BaseEntity(Guid.NewGuid())
    {
        public string ImageUrl { get; set; }
    }
}
