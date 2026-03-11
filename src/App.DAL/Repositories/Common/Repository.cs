using App.Core.Entities.Common;
using App.Core.Interfaces.Repository.Common;
using App.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.Repositories.Common;

public class Repository<TEntity>(AppDbContext context) : IRepository<TEntity> where TEntity : BaseEntity
{
    public DbSet<TEntity> Table => context.Set<TEntity>();
}