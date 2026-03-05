using App.Core.Interfaces.Common;

namespace App.Core.Interfaces;

/// <summary>
/// AuditableEntity-lər üçün genişləndirilmiş repository interfeysi.
/// Soft-delete, IsActive toggle və query filter bypass metodlarını əlavə edir.
/// </summary>
public interface IAuditableRepository<T> : IRepository<T>
    where T : AuditableEntity
{
    /// <summary>
    /// Query filter-i (IsDeleted, IsActive) keçərək id ilə axtarır.
    /// Silinmiş və ya deaktiv entity-lər üçün də işləyir.
    /// </summary>
    Task<T?> GetByIdIgnoringFiltersAsync(Guid id);

    /// <summary>
    /// Query filter-i keçərək bütün entity-ləri qaytarır.
    /// Silinmiş və deaktiv entity-lər daxil olmaqla hamısı.
    /// </summary>
    Task<IEnumerable<T>> GetAllIgnoringFiltersAsync();

    /// <summary>
    /// Entity-nin IsActive statusunu dəyişir.
    /// Query filter-i keçərək axtarır, sonra dəyişir və saxlayır.
    /// </summary>
    /// <param name="id">Entity-nin unikal identifikatoru.</param>
    /// <param name="isActive">Yeni aktiv status dəyəri.</param>
    /// <returns>Uğurlu olarsa true, entity tapılmadıqda false.</returns>
    Task<bool> SetActiveStatusAsync(Guid id, bool isActive);
}
