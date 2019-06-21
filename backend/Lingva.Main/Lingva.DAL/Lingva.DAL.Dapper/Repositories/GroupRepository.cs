using Lingva.DAL.Repositories;

namespace Lingva.DAL.Dapper.Repositories
{
    public class GroupRepository : Repository, IGroupRepository
    {
        public GroupRepository(DapperContext dbContext) : base(dbContext)
        {

        }
    }
}
