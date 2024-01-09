using FinCap.Domain.Business;
using FinCap.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FinCap.Application.Services
{
    public class ServiceUsuario
    {
        //private readonly IMapper _mapper;
        private readonly BusinessFactory _business;
        private readonly ServiceFactory _service;

        public ServiceUsuario(BusinessFactory business, ServiceFactory service)
        {
            _business = business;
            _service = service;
        }

        public Guid Cadastro(string nome, string email, string senha)
        {
            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha)) 
            {
                throw new Exception("Informações faltantes, verifique e tente novamente");
            }

            Usuario usuario = new()
            {
                Nome = nome,
                Email = email,
                Senha = senha
            };

            usuario = _business.Usuarios.Create(usuario);

            if (usuario == null)
            {
                throw new Exception("Não foi possível cadastrar o usuário");
            }

            return usuario.Uid;
        }

        public Usuario Get(Guid uidUsuario)
        {
            return _business.Usuarios.Get(uidUsuario);
        }
    }
}
