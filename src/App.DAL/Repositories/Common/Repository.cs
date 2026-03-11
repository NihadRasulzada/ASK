using App.Core.Entities.Common;
using App.Core.Interfaces.Repository.Common;
using App.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.DAL.Repositories.Common;

public class Repository<TEntity>(AppDbContext context) : IRepository<TEntity> where TEntity : BaseEntity
{
    public DbSet<TEntity> Table => context.Set<TEntity>();
}