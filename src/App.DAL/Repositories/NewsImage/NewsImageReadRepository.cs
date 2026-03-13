using App.Core.Interfaces.Repository.NewsImage;
using App.DAL.Context;
using App.DAL.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.DAL.Repositories.NewsImage;

public class NewsImageReadRepository : ReadRepository<Core.Entities.NewsImage>, INewsImageReadRepository
{
    public NewsImageReadRepository(AppDbContext context) : base(context)
    {
    }
}