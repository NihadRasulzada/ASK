namespace App.Core.Entities.Common;

public class AuditableEntity : BaseEntity
{
    public DateTimeOffset CreatedOn { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTimeOffset UpdatedOn { get; set; }
    public Guid UpdatedBy { get; set; }

    public bool IsDeleted { get; set; } = false;
    public bool IsActive { get; set; } = true;
}
