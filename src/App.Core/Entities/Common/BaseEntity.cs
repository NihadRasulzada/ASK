namespace App.Core.Entities.Common;

public abstract class BaseEntity(Guid id) : IBaseEntity
{
    public Guid Id { get; private set; } = id;

    public override int GetHashCode() => Id.GetHashCode();
}