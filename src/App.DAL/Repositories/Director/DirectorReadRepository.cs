using App.Core.Entities;
using App.Core.Interfaces.Repository.Director;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.Director;

public class DirectorReadRepository : SoftDeletableReadRepository<Core.Entities.Director>, IDirectorReadRepository
{
    public DirectorReadRepository(AppDbContext context) : base(context)
    {
    }
}