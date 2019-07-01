using Lingva.DAL.EF.Context;
using Lingva.DAL.Repositories;

namespace Lingva.DAL.EF.Repositories
{
    public class GroupRepository : Repository, IGroupRepository
    {
        public GroupRepository(DictionaryContext dbContext) : base(dbContext)
        {

        }
    }
}
