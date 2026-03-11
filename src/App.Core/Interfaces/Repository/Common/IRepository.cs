using App.Core.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace App.Core.Interfaces.Repository.Common;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    DbSet<TEntity> Table { get; }
}