using App.Core.Entities.Common;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace App.Core.Interfaces.Common;

public interface ISoftDeletableReadRepository<TEntity> : IReadRepository<TEntity>
    where TEntity : SoftDeletableEntity
{
    Task<TEntity?> GetByIdIncludingDeletedAsync(
        Guid id,
        bool enableTracking,
        CancellationToken cancellationToken,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

    Task<IEnumerable<TEntity>> GetAllIncludingDeletedAsync(
        CancellationToken cancellationToken,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);

    Task<IEnumerable<TEntity>> GetDeletedOnlyAsync(
        CancellationToken cancellationToken,
        Expression<Func<TEntity, bool>>? predicate = null);

    Task<bool> AnyIncludingDeletedAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken);
}