using Lingva.DAL.Context;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories.Contracts;

namespace Lingva.DAL.Repositories
{
    public class RepositoryWord : Repository<Word>, IRepositoryWord
    {
        public RepositoryWord(DictionaryContext context) : base(context)
        {

        }       
    }
}
