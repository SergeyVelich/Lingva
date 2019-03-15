using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;
using Lingva.DataAccessLayer.Repositories.Contracts;

namespace Lingva.DataAccessLayer.Repositories
{
    public class RepositoryDictionaryRecord : Repository<DictionaryRecord>, IRepositoryDictionaryRecord
    {
        public RepositoryDictionaryRecord(DictionaryContext context) : base(context)
        {

        }
    }
}
