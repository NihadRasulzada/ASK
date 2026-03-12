using App.Core.Entities.Common;

namespace App.Core.Interfaces.Repository.Common;

public interface IWriteRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

    Task HardDeleteAsync(Guid id, CancellationToken cancellationToken);
    Task HardDeleteRangeAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

    void Update(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}