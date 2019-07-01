using System.Collections.Generic;
using System.Threading.Tasks;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;

namespace Lingva.DAL.CosmosSqlApi.Repositories
{
    public class UserRepository : Repository, IUserRepository
    {
        public UserRepository(CosmosSqlApiContext dbContext) : base(dbContext)
        {

        }

        public Task<IEnumerable<User>> GetListByGroupAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
