using App.Core.Entities;
using App.Core.Interfaces.Repository.Training;
using App.DAL.Context;
using App.DAL.Repositories.Common;

namespace App.DAL.Repositories.Training;

public class TrainingWriteRepository(AppDbContext context, ITrainingReadRepository readRepository) : SoftDeletableWriteRepository<Core.Entities.Training>(context, readRepository), ITrainingWriteRepository
{
}
