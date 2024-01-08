using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FinCap.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where);
        IEnumerable<T> Query<T>(string sql);
        TEntity Find(Expression<Func<TEntity, bool>> where);
        int FindSQL(string query);
        TEntity Create(TEntity model);
        void BulkCreate(IEnumerable<TEntity> models);
        int Save();
        TEntity Update(TEntity model);
        bool Delete(TEntity model);
    }
}
