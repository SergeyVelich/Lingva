using Lingva.DAL.Entities;
using QueryBuilder.QueryOptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.DAL.Repositories
{
    public interface IRepository
    {
        Task<IEnumerable<T>> GetListAsync<T>() where T : BaseBE, new();
        Task<IEnumerable<T>> GetListAsync<T>(IQueryOptions queryOptions) where T : BaseBE, new();
        Task<int> CountAsync<T>(IQueryOptions queryOptions) where T : BaseBE, new();
        Task<T> GetByIdAsync<T>(int id) where T : BaseBE, new();

        Task<T> CreateAsync<T>(T entity) where T : BaseBE, new();
        Task<T> UpdateAsync<T>(T entity) where T : BaseBE, new();
        Task DeleteAsync<T>(int id) where T : BaseBE, new();
    }
}
