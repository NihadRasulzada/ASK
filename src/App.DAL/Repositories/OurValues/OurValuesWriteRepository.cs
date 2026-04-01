using App.Core.Interfaces.Repository.OurValues;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.OurValues;

public class OurValuesWriteRepository : WriteRepository<Core.Entities.OurValues>, IOurValuesWriteRepository
{
    public OurValuesWriteRepository(AppDbContext context, IOurValuesReadRepository readRepository)
        : base(context, readRepository)
    {
    }
}
