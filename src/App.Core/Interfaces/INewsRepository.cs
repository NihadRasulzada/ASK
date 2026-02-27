using App.Core.Entities;

namespace App.Core.Interfaces;

public interface INewsRepository : IAuditableRepository<News>
{
    // News-ə xüsusi sorğular gələcəkdə buraya əlavə edilə bilər.
}
