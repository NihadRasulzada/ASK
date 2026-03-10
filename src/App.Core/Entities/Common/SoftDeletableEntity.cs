namespace App.Core.Entities.Common;

public abstract class SoftDeletableEntity(Guid id, bool isDeactive) : BaseEntity(id)
{
    public bool IsDeactive { get; protected set; } = isDeactive;

    public void Deactivate() => IsDeactive = true;
    public void Activate() => IsDeactive = false;
}