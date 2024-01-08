using FinCap.Domain.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinCap.Application.Services
{
    public class ServiceFactory : IDisposable
    {
        //private readonly IMapper _mapper;
        private readonly BusinessFactory _business;

        public ServiceFactory(BusinessFactory business)
        {
            _business = business;
        }

        private ServiceUsuario _usuarios;
        public virtual ServiceUsuario Usuarios => _usuarios ?? (_usuarios = new ServiceUsuario(_business, this));

        public void Dispose() { }
    }
}
