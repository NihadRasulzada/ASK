using App.Core.Entities;
using App.Core.Interfaces;
using App.DAL.Context;

namespace App.DAL.Repositories;

public class DirectorRepository : Repository<Director>, IDirectorRepository
{
    public DirectorRepository(AppDbContext context) : base(context)
    {
    }
}
