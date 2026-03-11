using App.Core.Interfaces.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Interfaces.Repository.Director;

public interface IDirectorWriteRepository : ISoftDeletableWriteRepository<Entities.Director>
{
}