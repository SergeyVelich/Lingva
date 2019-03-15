using Lingva.DAL.Context;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories.Contracts;

namespace Lingva.DAL.Repositories
{
    public class RepositoryDictionaryRecord : Repository<DictionaryRecord>, IRepositoryDictionaryRecord
    {
        public RepositoryDictionaryRecord(DictionaryContext context) : base(context)
        {

        }
    }
}
