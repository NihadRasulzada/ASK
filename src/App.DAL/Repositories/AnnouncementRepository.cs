using App.Core.Entities;
using App.Core.Interfaces;
using App.DAL.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.DAL.Repositories
{
    public class AnnouncementRepository(AppDbContext context) : Repository<Announcement>(context), IAnnouncementRepository
    {
    }
}
