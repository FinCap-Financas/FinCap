using FinCap.Data.Context;
using FinCap.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using FinCap.Encryption;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq.Expressions;
using Newtonsoft.Json;
using FinCap.Domain.Interfaces;
using System;

namespace FinCap.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
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

        public void BulkCreate(IEnumerable<TEntity> models)
        {
            DbSet.AddRange(models);
            Save();
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where)
        {
            return DbSet.Where(where).AsQueryable().AsNoTracking();
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, object> includes)
        {
            IQueryable<TEntity> _query = DbSet;

            if (includes != null)
                _query = includes(_query) as IQueryable<TEntity>;

            return _query.Where(where).AsQueryable().AsNoTracking();
        }

        public IEnumerable<T> Query<T>(string sql)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(_contexto.Database.GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            var data = dataTable.Rows.OfType<DataRow>()
                .Select(row => dataTable.Columns.OfType<DataColumn>()
                    .ToDictionary(col => col.ColumnName, c => row[c]));

            var json = JsonConvert.SerializeObject(data);

            return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
        }

        public TEntity Find(Expression<Func<TEntity, bool>> where)
        {
            return DbSet.AsNoTracking().FirstOrDefault(where);
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, object> includes)
        {
            IQueryable<TEntity> _query = DbSet;

            if (includes != null)
                _query = includes(_query) as IQueryable<TEntity>;

            return _query.AsNoTracking().FirstOrDefault(predicate);
        }

        public int FindSQL(string query)
        {
            return _contexto.Database.ExecuteSqlInterpolated($"{query}");
        }

        public TEntity Update(TEntity model)
        {
            (model as Entidade).DataAtualizacao = DateTime.Now;
            var _entry = _contexto.Entry(model);

            DbSet.Attach(model);

            _entry.State = EntityState.Modified;

            Save();

            return _entry.Entity;
        }

        public bool Delete(TEntity model)
        {
            (model as Entidade).Deletado = true;

            return Update(model) != null;
        }

        public int Save()
        {
            int save = _contexto.SaveChanges();
            _contexto.ChangeTracker.Clear();
            return save;
        }

        public void Dispose()
        {
            if (_contexto != null)
                _contexto.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
