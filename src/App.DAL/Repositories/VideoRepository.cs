using App.Core.Entities;
using App.Core.Interfaces;
using App.DAL.Context;

namespace App.DAL.Repositories;

public class VideoRepository : Repository<Video>, IVideoRepository
{
    public VideoRepository(AppDbContext context) : base(context)
    {
    }
}
