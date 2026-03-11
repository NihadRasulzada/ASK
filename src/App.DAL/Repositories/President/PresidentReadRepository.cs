using App.Core.Entities;
using App.Core.Interfaces.Repository.President;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.President;

public class PresidentReadRepository : ReadRepository<Core.Entities.President>, IPresidentReadRepository
{
    public PresidentReadRepository(AppDbContext context) : base(context)
    {
    }
}