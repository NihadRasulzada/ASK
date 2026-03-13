using App.Core.Entities;
using App.Core.Interfaces.Repository.Gallery;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.Gallery;

public class GalleryReadRepository : ReadRepository<Core.Entities.Gallery>, IGalleryReadRepository
{
    public GalleryReadRepository(AppDbContext context) : base(context)
    {
    }
}
