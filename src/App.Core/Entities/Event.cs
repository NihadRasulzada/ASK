using App.Core.Entities.Common;

namespace App.Core.Entities;

public class Event() : SoftDeletableEntity(Guid.NewGuid(), false)
{
    public string Title { get; set; }
    public string TitleImageUrl { get; set; }
    public string Text { get; set; }
}