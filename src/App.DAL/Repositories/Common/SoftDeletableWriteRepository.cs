using App.Core.Entities.Common;
using App.Core.Interfaces.Common;
using App.DAL.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.DAL.Repositories.Common;

public class SoftDeletableWriteRepository<TEntity>(AppDbContext context, ISoftDeletableReadRepository<TEntity> softDeletableReadRepository)
    : WriteRepository<TEntity>(context, softDeletableReadRepository), IWriteRepository<TEntity>
    where TEntity : SoftDeletableEntity
{
    public override async Task HardDeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await softDeletableReadRepository.GetByIdIncludingDeletedAsync(id, true, cancellationToken: cancellationToken)
             .ContinueWith(task =>
             {
                 if (task.Result is not null)
                 {
                     Table.Remove(task.Result);
                 }
             }, cancellationToken);
    }

    public override Task HardDeleteRangeAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        var entities = new List<TEntity>();
        return Task.WhenAll(ids.Select(id =>
            softDeletableReadRepository.GetByIdIncludingDeletedAsync(id, true, cancellationToken: cancellationToken)
                .ContinueWith(task =>
                {
                    if (task.Result is not null)
                    {
                        entities.Add(task.Result);
                    }
                }, cancellationToken)
        )).ContinueWith(_ => Table.RemoveRange(entities), cancellationToken);
    }
}