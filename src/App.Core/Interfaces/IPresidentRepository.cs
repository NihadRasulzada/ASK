using App.Core.Entities;
using App.Core.Interfaces.Common;

namespace App.Core.Interfaces;

public interface IPresidentRepository : IRepository<President>
{
    Task<President?> GetSingleAsync();
}
