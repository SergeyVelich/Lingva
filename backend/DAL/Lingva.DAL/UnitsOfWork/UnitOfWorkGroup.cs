using Lingva.DAL.Context;
using Lingva.DAL.Repositories.Contracts;
using Lingva.DAL.UnitsOfWork.Contracts;

namespace Lingva.DAL.UnitsOfWork
{
    public class UnitOfWorkGroup : UnitOfWork, IUnitOfWorkGroup
    {
        private readonly IRepositoryGroup _groups;

        public UnitOfWorkGroup(DictionaryContext context, IRepositoryGroup groups) : base(context)
        {
            _groups = groups;
        }

        public IRepositoryGroup Groups { get => _groups; }
    }
}
