namespace App.Core.Entities.Common;

public class AuditableEntity : BaseEntity
{
    public DateTimeOffset CreatedOn { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTimeOffset UpdatedOn { get; set; }
    public Guid UpdatedBy { get; set; }

    public bool IsDeleted { get; set; } = false;
    public bool IsActive { get; set; } = true;

    /// <summary>Entiti-ni məntiqi olaraq silir (soft delete).</summary>
    public void SoftDelete() => IsDeleted = true;

    /// <summary>Silinmiş entiti-ni bərpa edir.</summary>
    public void Restore() => IsDeleted = false;

    /// <summary>Entiti-ni aktiv edir.</summary>
    public void Activate() => IsActive = true;

    /// <summary>Entiti-ni deaktiv edir.</summary>
    public void Deactivate() => IsActive = false;
}
