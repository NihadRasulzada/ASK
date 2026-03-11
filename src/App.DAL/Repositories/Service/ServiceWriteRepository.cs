using App.Core.Interfaces.Repository.Common;
using App.Core.Interfaces.Repository.Service;
using App.DAL.Context;
using App.DAL.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.DAL.Repositories.Service;

public class ServiceWriteRepository : SoftDeletableWriteRepository<Core.Entities.Service>, IServiceWriteRepository
{
    public ServiceWriteRepository(AppDbContext context, ISoftDeletableReadRepository<Core.Entities.Service> readRepository) : base(context, readRepository)
    {
    }
}