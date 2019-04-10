using Lingva.DAL.EF.Context;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories.Contracts;

namespace Lingva.DAL.EF.Repositories
{
    public class RepositoryLanguage : Repository<Language>, IRepositoryLanguage
    {
        public RepositoryLanguage(DictionaryContext context) : base(context)
        {

        }
    }
}
