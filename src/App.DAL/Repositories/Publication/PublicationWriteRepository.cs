using App.Core.Interfaces.Repository.Publication;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.Publication;

public class PublicationWriteRepository(AppDbContext context, IPublicationReadRepository readRepository)
    : WriteRepository<Core.Entities.Publication>(context, readRepository), IPublicationWriteRepository
{
}
