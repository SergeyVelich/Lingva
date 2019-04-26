using Lingva.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.DAL.Repositories
{
    public interface IUserRepository : IRepository
    {
        Task<IEnumerable<User>> GetListByGroupAsync(int id);
    }
}
