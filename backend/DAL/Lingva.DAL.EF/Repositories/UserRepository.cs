using Lingva.DAL.EF.Context;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using QueryBuilder.Extensions;
using QueryBuilder.QueryOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lingva.DAL.EF.Repositories
{
    public class UserRepository : Repository, IUserRepository
    {
        public UserRepository(DictionaryContext dbContext) : base(dbContext)
        {

        }

        public virtual async Task<IEnumerable<User>> GetListByGroupAsync(int id)
        {
            return _dbContext.Set<GroupUser>().Where(gu => gu.GroupId == id).Include(gu => gu.User).Select(gu => gu.User).ToList();
        }
    }
}
