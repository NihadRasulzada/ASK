using App.Core.Entities.Common;
using App.Core.Interfaces;
using App.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.Repositories;

/// <summary>
/// AuditableEntity-lər üçün genişləndirilmiş repository.
/// Soft-delete, IsActive toggle, audit query filter bypass metodlarını implementasiya edir.
/// </summary>
public class AuditableRepository<T> : Repository<T>, IAuditableRepository<T>
    where T : AuditableEntity
{
    public AuditableRepository(AppDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Query filter-ə hörmət edərək id ilə axtarır.
    /// FindAsync query filter-i keçdiyindən FirstOrDefaultAsync istifadə olunur.
    /// </summary>
    public override async Task<T?> GetByIdAsync(Guid id)
        => await _dbSet.FirstOrDefaultAsync(e => e.Id == id);

    /// <summary>
    /// Soft delete — entity-ni DB-dən silmir, IsDeleted = true edir.
    /// Service tərəfindən SaveChangesAsync çağırılır.
    /// </summary>
    public override async Task DeleteAsync(Guid id)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        if (entity is null) return;

        entity.SoftDelete();
        _dbSet.Update(entity);
    }

    /// <inheritdoc/>
    public async Task<T?> GetByIdIgnoringFiltersAsync(Guid id)
        => await _dbSet.IgnoreQueryFilters().FirstOrDefaultAsync(e => e.Id == id);

    /// <inheritdoc/>
    public async Task<IEnumerable<T>> GetAllIgnoringFiltersAsync()
        => await _dbSet.IgnoreQueryFilters().ToListAsync();

    /// <inheritdoc/>
    public async Task<bool> SetActiveStatusAsync(Guid id, bool isActive)
    {
        var entity = await _dbSet.IgnoreQueryFilters().FirstOrDefaultAsync(e => e.Id == id);
        if (entity is null) return false;

        if (isActive)
            entity.Activate();
        else
            entity.Deactivate();

        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
