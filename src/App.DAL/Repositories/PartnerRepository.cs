using App.Core.Entities;
using App.Core.Interfaces;
using App.DAL.Context;

namespace App.DAL.Repositories;

public class PartnerRepository : Repository<Partner>, IPartnerRepository
{
    public PartnerRepository(AppDbContext context) : base(context)
    {
    }
}
