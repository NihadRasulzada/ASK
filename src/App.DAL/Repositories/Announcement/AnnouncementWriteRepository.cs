using App.Core.Entities;
using App.Core.Interfaces.Repository.Announcement;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.Announcement;

public class AnnouncementWriteRepository : WriteRepository<Core.Entities.Announcement>, IAnnouncementWriteRepository
{
    public AnnouncementWriteRepository(AppDbContext context, IAnnouncementReadRepository readRepository) : base(context, readRepository)
    {
    }
}
