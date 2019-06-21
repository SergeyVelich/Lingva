using Lingva.DAL.Repositories;

namespace Lingva.DAL.CosmosSqlApi.Repositories
{
    public class GroupRepository : Repository, IGroupRepository
    {
        public GroupRepository(CosmosSqlApiContext dbContext) : base(dbContext)
        {

        }
    }
}
