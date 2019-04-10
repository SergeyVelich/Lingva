using Lingva.DAL.EF.Context;
using Lingva.DAL.Repositories.Contracts;
using Lingva.DAL.UnitsOfWork.Contracts;

namespace Lingva.DAL.EF.UnitsOfWork
{
    public class UnitOfWorkInfo : UnitOfWork, IUnitOfWorkInfo
    {
        private readonly IRepositoryLanguage _languages;

        public UnitOfWorkInfo(DictionaryContext context, IRepositoryLanguage languages) : base(context)
        {
            _languages = languages;
        }

        public IRepositoryLanguage Languages { get => _languages; }
    }
}
