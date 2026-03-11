using App.Core.Interfaces.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Interfaces.Repository.Service;

public interface IServiceWriteRepository : ISoftDeletableWriteRepository<Entities.Service>
{
}