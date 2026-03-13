using App.Core.Entities;
using App.Core.Interfaces.Repository.Common;

namespace App.Core.Interfaces.Repository.Training;

public interface ITrainingReadRepository : ISoftDeletableReadRepository<Core.Entities.Training>
{
}
