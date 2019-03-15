using Lingva.DAL.Context;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories.Contracts;

namespace Lingva.DAL.Repositories
{
    public class RepositoryGroup : Repository<Group>, IRepositoryGroup
    {
        public RepositoryGroup(DictionaryContext context) : base(context)
        {

        }
    }
}
