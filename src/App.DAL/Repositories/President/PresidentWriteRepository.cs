using App.Core.Interfaces.Repository.Common;
using App.Core.Interfaces.Repository.President;
using App.DAL.Context;
using App.DAL.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.DAL.Repositories.President;

public class PresidentWriteRepository : WriteRepository<Core.Entities.President>, IPresidentWriteRepository
{
    public PresidentWriteRepository(AppDbContext context, IReadRepository<Core.Entities.President> readRepository) : base(context, readRepository)
    {
    }
}