using Lingva.DAL.Repositories.Contracts;
using Lingva.DAL.UnitsOfWork.Contracts;

namespace Lingva.DAL.Dapper.UnitsOfWork
{
    public class UnitOfWorkGroup : UnitOfWork, IUnitOfWorkGroup
    {
        private readonly IRepositoryGroup _groups;

        public UnitOfWorkGroup(IConnectionFactory connectionFactory, IRepositoryGroup groups) : base(connectionFactory)
        {
            _groups = groups;
        }

        public IRepositoryGroup Groups { get => _groups; }
    }
}
