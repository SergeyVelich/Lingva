using Lingva.DAL.EF.Context;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories.Contracts;

namespace Lingva.DAL.EF.Repositories
{
    public class RepositoryGroup : Repository<Group>, IRepositoryGroup
    {
        public RepositoryGroup(DictionaryContext context) : base(context)
        {

        }
    }
}
