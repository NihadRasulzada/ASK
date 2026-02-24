namespace App.Core.Entities;

public class Partner : AuditableEntity
{
    public string IconUrl { get; set; } = null!;
    public string SiteUrl { get; set; } = null!;    
}
