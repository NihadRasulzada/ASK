using App.Core.Entities;
using App.Core.Interfaces.Repository.Common;
using App.Core.Interfaces.Repository.Gallery;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.Gallery;

public class GalleryWriteRepository : WriteRepository<Core.Entities.Gallery>, IGalleryWriteRepository
{
    public GalleryWriteRepository(AppDbContext context, IGalleryReadRepository readRepository) : base(context, readRepository)
    {
    }
}
