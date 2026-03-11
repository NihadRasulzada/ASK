using App.Core.Interfaces.Repository.Common;
using App.Core.Interfaces.Repository.Director;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.Director;

public class DirectorWriteRepository : SoftDeletableWriteRepository<Core.Entities.Director>, IDirectorWriteRepository
{
    public DirectorWriteRepository(AppDbContext context, ISoftDeletableReadRepository<Core.Entities.Director> softDeletableReadRepository) : base(context, softDeletableReadRepository)
    {
    }
}
