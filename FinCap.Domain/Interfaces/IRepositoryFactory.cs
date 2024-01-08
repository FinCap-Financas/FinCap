using FinCap.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinCap.Domain.Interfaces
{
    public interface IRepositoryFactory : IDisposable
    {
        IRepository<Usuario> Usuarios { get; }
    }
}
