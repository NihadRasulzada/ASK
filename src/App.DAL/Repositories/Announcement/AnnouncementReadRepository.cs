using App.Core.Entities;
using App.Core.Interfaces.Repository.Announcement;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.Announcement;

public class AnnouncementReadRepository : ReadRepository<Core.Entities.Announcement>, IAnnouncementReadRepository
{
    public AnnouncementReadRepository(AppDbContext context) : base(context)
    {
    }
}
