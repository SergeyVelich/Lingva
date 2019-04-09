using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lingva.DAL.Repositories.Contracts
{
    public interface IRepository<T>
        where T : class
    {
        IEnumerable<T> GetList(Expression<Func<T, bool>> predicator);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicator = null);
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        T Get(Expression<Func<T, bool>> predicator);
        Task<T> GetAsync(Expression<Func<T, bool>> predicator);

        T Create(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}
