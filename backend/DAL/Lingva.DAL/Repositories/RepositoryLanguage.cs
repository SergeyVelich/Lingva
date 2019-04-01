using Lingva.DAL.Context;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories.Contracts;

namespace Lingva.DAL.Repositories
{
    public class RepositoryLanguage : Repository<Language>, IRepositoryLanguage
    {
        public RepositoryLanguage(DictionaryContext context) : base(context)
        {

        }
    }
}
