using App.Core.Entities;
using App.Core.Interfaces.Repository.Common;

namespace App.Core.Interfaces.Repository.Exhibition;

public interface IExhibitionReadRepository : ISoftDeletableReadRepository<Core.Entities.Exhibition>
{
}
