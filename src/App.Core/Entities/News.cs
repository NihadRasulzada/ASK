namespace App.Core.Entities;

public class News : AuditableEntity
{
    public string TitleImageUrl { get; set; }
    public string NewsText { get; set; }
    public IEnumerable<string> ImageUrls { get; set; } = Array.Empty<string>();
}