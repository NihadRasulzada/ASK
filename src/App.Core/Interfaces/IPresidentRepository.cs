using App.Core.Entities;

namespace App.Core.Interfaces;

public interface IPresidentRepository : IRepository<President>
{
    Task<President?> GetSingleAsync();
}
