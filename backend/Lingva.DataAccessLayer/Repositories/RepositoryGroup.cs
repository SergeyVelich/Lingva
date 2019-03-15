using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;
using Lingva.DataAccessLayer.Repositories.Contracts;

namespace Lingva.DataAccessLayer.Repositories
{
    public class RepositoryGroup : Repository<Group>, IRepositoryGroup
    {
        public RepositoryGroup(DictionaryContext context) : base(context)
        {

        }
    }
}
