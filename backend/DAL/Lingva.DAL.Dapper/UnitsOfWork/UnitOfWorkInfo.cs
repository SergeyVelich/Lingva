using Lingva.DAL.Repositories.Contracts;
using Lingva.DAL.UnitsOfWork.Contracts;

namespace Lingva.DAL.Dapper.UnitsOfWork
{
    public class UnitOfWorkInfo : UnitOfWork, IUnitOfWorkInfo
    {
        private readonly IRepositoryLanguage _languages;

        public UnitOfWorkInfo(IConnectionFactory connectionFactory, IRepositoryLanguage languages) : base(connectionFactory)
        {
            _languages = languages;
        }

        public IRepositoryLanguage Languages { get => _languages; }
    }
}
