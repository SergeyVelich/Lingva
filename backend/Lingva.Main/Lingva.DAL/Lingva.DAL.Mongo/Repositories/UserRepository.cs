using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.DAL.Mongo.Repositories
{
    public class UserRepository : Repository, IUserRepository
    {
        public UserRepository(MongoContext dbContext) : base(dbContext)
        {

        }

        public Task<IEnumerable<User>> GetListByGroupAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
