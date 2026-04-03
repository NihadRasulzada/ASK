using App.Core.Interfaces.Repository.Publication;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.Publication;

public class PublicationReadRepository(AppDbContext context)
    : ReadRepository<Core.Entities.Publication>(context), IPublicationReadRepository
{
}
