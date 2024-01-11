using FinCap.Domain.Entities;
using FinCap.Domain.Interfaces;
using FinCap.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinCap.Domain.Business
{
    public class UsuarioBusiness
    {
        private readonly IRepository<Usuario> _repository;
        private readonly BusinessFactory _business;

        public UsuarioBusiness(IRepository<Usuario> repository, BusinessFactory business)
        {
            _repository = repository;
            _business = business;
        }

        public Usuario Get(Guid uid)
        {
            return _repository.Find(usuario => usuario.Uid == uid);
        }

        public Usuario Create(Usuario usuario)
        {
            var novoUsuario = _repository.Create(usuario);
            return novoUsuario;
        }

        public Usuario Login(string email, string senha)
        {
            var usuario = _repository.Find(usuario => usuario.Email == email);

            if (usuario is null)
            {
                throw new Exception("Email ou senha inválidos, tente novamente");
            }

            string senhaCriptografada = Encryptor.Encrypt(usuario.Email, senha);

            if (usuario.Senha != senhaCriptografada)
            {
                throw new Exception("Email ou senha inválidos, tente novamente");
            }

            return usuario;
        }
    }
}
