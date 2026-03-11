using App.Core.Interfaces.Repository.Video;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.Video;

public class VideoReadRepository : ReadRepository<Core.Entities.Video>, IVideoReadRepository
{
    public VideoReadRepository(AppDbContext context) : base(context)
    {
    }
}