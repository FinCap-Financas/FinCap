using FinCap.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinCap.Domain.Business
{
    public class BusinessFactory : IDisposable
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public BusinessFactory(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        private UsuarioBusiness _usuarios;
        public virtual UsuarioBusiness Usuarios => _usuarios ?? (_usuarios = new UsuarioBusiness(_repositoryFactory.Usuarios, this));

        public void Dispose() { }
    }
}
