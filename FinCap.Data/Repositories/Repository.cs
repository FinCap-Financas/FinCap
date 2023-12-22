using FinCap.Data.Context;
using FinCap.Domain.Entities;
using FinCap.Encryption;
using Microsoft.EntityFrameworkCore;

namespace FinCap.Data.Repositories
{
    public class Repository<TEntity> where TEntity : class
    {
        protected readonly FinCapDbContext _contexto;

        protected DbSet<TEntity> DbSet
        {
            get
            {
                return _contexto.Set<TEntity>();
            }
        }

        public Repository(FinCapDbContext contexto)
        {
            _contexto = contexto;
        }

        public TEntity Create(TEntity model)
        {
            if (model is Usuario)
                (model as Usuario).Senha = Encryptor.Encrypt((model as Usuario).Email, (model as Usuario).Senha);

            DbSet.Attach(model);
            Save();
            return model;
        }

        public int Save()
        {
            int save = _contexto.SaveChanges();
            _contexto.ChangeTracker.Clear();
            return save;
        }


    }
}
