using App.Core.Entities.Common;
using App.Core.Interfaces.Repository.Common;
using App.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace App.DAL.Repositories.Common;

public class SoftDeletableReadRepository<TEntity>(AppDbContext context)
    : ReadRepository<TEntity>(context), ISoftDeletableReadRepository<TEntity>
    where TEntity : SoftDeletableEntity
{
    public Task<bool> AnyIncludingDeletedAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        context.IgnoreSoftDeleteFilter = true;
        try
        {
            return Table.AnyAsync(predicate, cancellationToken);
        }
        finally
        {
            context.IgnoreSoftDeleteFilter = false;
        }
    }

    public async Task<IEnumerable<TEntity>> GetAllIncludingDeletedAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
    {
        IQueryable<TEntity> query = Table.AsNoTracking();

        if (predicate != null)
            query = query.Where(predicate);

        if (include != null)
            query = include(query);

        if (orderBy != null)
            query = orderBy(query);

        context.IgnoreSoftDeleteFilter = true;

        try
        {
            return await query.ToListAsync(cancellationToken);
        }
        finally
        {
            context.IgnoreSoftDeleteFilter = false;
        }
    }

    public async Task<TEntity?> GetByIdIncludingDeletedAsync(Guid id, bool enableTracking, CancellationToken cancellationToken, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        IQueryable<TEntity> query = Table;

        if (!enableTracking)
            query = query.AsNoTracking();

        if (include != null)
            query = include(query);

        context.IgnoreSoftDeleteFilter = true;

        try
        {
            return await query.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }
        finally
        {
            context.IgnoreSoftDeleteFilter = false;
        }
    }

    public async Task<IEnumerable<TEntity>> GetDeletedOnlyAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>>? predicate = null)
    {
        context.IgnoreSoftDeleteFilter = true;

        IQueryable<TEntity> query = Table.Where(e => e.IsDeactive);

        if (predicate != null)
            query = query.Where(predicate);

        try
        {
            return await query.ToListAsync(cancellationToken);
        }
        finally
        {
            context.IgnoreSoftDeleteFilter = false;
        }
    }
}