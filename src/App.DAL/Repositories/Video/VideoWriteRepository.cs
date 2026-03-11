using App.Core.Interfaces.Repository.Common;
using App.Core.Interfaces.Repository.Video;
using App.DAL.Context;
using App.DAL.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.DAL.Repositories.Video;

public class VideoWriteRepository : WriteRepository<Core.Entities.Video>, IVideoWriteRepository
{
    public VideoWriteRepository(AppDbContext context, IReadRepository<Core.Entities.Video> readRepository) : base(context, readRepository)
    {
    }
}