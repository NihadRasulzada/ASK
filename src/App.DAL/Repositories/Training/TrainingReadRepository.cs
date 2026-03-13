using App.Core.Entities;
using App.Core.Interfaces.Repository.Training;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.Training;

public class TrainingReadRepository(AppDbContext context) : SoftDeletableReadRepository<Core.Entities.Training>(context), ITrainingReadRepository
{
}
