using App.Core.Entities;
using App.Core.Interfaces.Repository.Exhibition;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.Exhibition;

public class ExhibitionReadRepository(AppDbContext context) : SoftDeletableReadRepository<Core.Entities.Exhibition>(context), IExhibitionReadRepository
{
}
