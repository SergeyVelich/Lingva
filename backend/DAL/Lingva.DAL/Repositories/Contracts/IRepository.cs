using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lingva.DAL.Repositories.Contracts
{
    public interface IRepository<T>
        where T : class
    {
        IEnumerable<T> GetList(Expression<Func<T, bool>> predicator = null, IEnumerable<string> sorters = null, IEnumerable<Expression<Func<T, bool>>> includers = null, int skip = 0, int take = 0);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicator = null, IEnumerable<string> sorters = null, ICollection<Expression<Func<T, bool>>> includers = null, int skip = 0, int take = 0);
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        T Get(Expression<Func<T, bool>> predicator);
        Task<T> GetAsync(Expression<Func<T, bool>> predicator);

        T Create(T entity);
        T Update(T entity);
        void Delete(T entity);

        Task<int> CountAsync(Expression<Func<T, bool>> predicator = null, IEnumerable<string> sorters = null, int skip = 0, int take = 0);
        int Count(Expression<Func<T, bool>> predicator = null, IEnumerable<string> sorters = null, int skip = 0, int take = 0);
    }
}
