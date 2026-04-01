using App.Core.Interfaces.Repository.OurValues;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.OurValues;

public class OurValuesReadRepository : ReadRepository<Core.Entities.OurValues>, IOurValuesReadRepository
{
    public OurValuesReadRepository(AppDbContext context) : base(context)
    {
    }
}
