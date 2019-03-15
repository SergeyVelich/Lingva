using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;
using Lingva.DataAccessLayer.Repositories.Contracts;

namespace Lingva.DataAccessLayer.Repositories
{
    public class RepositoryWord : Repository<Word>, IRepositoryWord
    {
        public RepositoryWord(DictionaryContext context) : base(context)
        {

        }       
    }
}
