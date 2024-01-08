using FinCap.Data.Context;
using FinCap.Domain.Entities;
using FinCap.Domain.Interfaces;

namespace FinCap.Data.Repositories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly FinCapDbContext _contexto;

        public RepositoryFactory(FinCapDbContext contexto)
        {
            _contexto = contexto;
        }

        private Repository<Categoria> _categoria;
        public virtual IRepository<Categoria> Categorias => _categoria ?? (_categoria = new Repository<Categoria>(_contexto));

        private Repository<Conta> _conta;
        public virtual IRepository<Conta> Contas => _conta ?? (_conta = new Repository<Conta>(_contexto));

        private Repository<Transacao> _transacao;
        public virtual IRepository<Transacao> Transacoes => _transacao ?? (_transacao = new Repository<Transacao>(_contexto));

        private Repository<Usuario> _usuario;
        public virtual IRepository<Usuario> Usuarios => _usuario ?? (_usuario = new Repository<Usuario>(_contexto));

        public void Dispose() { }
    }
}
