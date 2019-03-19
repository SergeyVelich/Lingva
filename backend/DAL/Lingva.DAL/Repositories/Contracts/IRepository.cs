using System;
using System.Linq;
using System.Linq.Expressions;

namespace Lingva.DAL.Repositories.Contracts
{
    public interface IRepository<T>
        where T : class
    {
        IQueryable<T> GetList();
        IQueryable<T> GetList(Expression<Func<T, bool>> predicator);

        T Get(object id);
        T Get(Expression<Func<T, bool>> predicator);

        T Create(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}
