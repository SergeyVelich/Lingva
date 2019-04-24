using QueryBuilder.QueryOptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.DAL.Repositories
{
    public interface IRepository
    {
        Task<IEnumerable<T>> GetListAsync<T>() where T : class, new();
        Task<IEnumerable<T>> GetListAsync<T>(IQueryOptions queryOptions) where T : class, new();
        Task<T> GetByIdAsync<T>(int id) where T : class, new();

        Task<T> CreateAsync<T>(T entity) where T : class, new();
        Task<T> UpdateAsync<T>(T entity) where T : class, new();
        Task DeleteAsync<T>(T entity) where T : class, new();
    }
}
