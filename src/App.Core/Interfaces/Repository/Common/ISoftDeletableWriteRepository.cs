using App.Core.Entities.Common;

namespace App.Core.Interfaces.Repository.Common;

public interface ISoftDeletableWriteRepository<TEntity> : IWriteRepository<TEntity>
    where TEntity : SoftDeletableEntity
{
    Task HardDeleteIncludingDeletedAsync(Guid id, CancellationToken cancellationToken);
    Task HardDeleteRangeIncludingDeletedAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
}