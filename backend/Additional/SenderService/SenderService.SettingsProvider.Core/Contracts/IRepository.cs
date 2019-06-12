using System.Collections.Generic;
using System.Threading.Tasks;

namespace SenderService.SettingsProvider.Core.Contracts
{
    public interface IRepository
    {
        Task<IEnumerable<T>> GetListAsync<T>() where T : class, new();
        Task<T> GetByIdAsync<T>(int id) where T : class, new();

        Task<T> CreateAsync<T>(T entity) where T : class, new();
        Task<T> UpdateAsync<T>(T entity) where T : class, new();
        Task DeleteAsync<T>(T entity) where T : class, new();
    }
}
