using App.Core.Interfaces.Repository.Common;
using App.Core.Interfaces.Repository.Video;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.Video;

public class VideoWriteRepository : WriteRepository<Core.Entities.Video>, IVideoWriteRepository
{
    public VideoWriteRepository(AppDbContext context, IVideoReadRepository readRepository) : base(context, readRepository)
    {
    }
}