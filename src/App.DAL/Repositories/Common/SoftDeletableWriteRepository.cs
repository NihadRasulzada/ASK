using App.Core.Entities.Common;
using App.Core.Interfaces.Repository.Common;
using App.DAL.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.DAL.Repositories.Common;

public class SoftDeletableWriteRepository<TEntity>(AppDbContext context, ISoftDeletableReadRepository<TEntity> softDeletableReadRepository)
    : WriteRepository<TEntity>(context, softDeletableReadRepository), ISoftDeletableWriteRepository<TEntity>
    where TEntity : SoftDeletableEntity
{
    public async Task HardDeleteIncludingDeletedAsync(Guid id, CancellationToken cancellationToken)
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

    public Task HardDeleteRangeIncludingDeletedAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
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