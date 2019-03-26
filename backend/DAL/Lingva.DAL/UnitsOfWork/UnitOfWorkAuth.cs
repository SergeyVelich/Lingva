using Lingva.DAL.Context;
using Lingva.DAL.Repositories.Contracts;
using Lingva.DAL.UnitsOfWork.Contracts;

namespace Lingva.DAL.UnitsOfWork
{
    public class UnitOfWorkAuth : UnitOfWork, IUnitOfWorkAuth
    {
        private readonly IRepositoryUser _users;

        public UnitOfWorkAuth(DictionaryContext context, IRepositoryUser users) : base(context)
        {
            _users = users;
        }

        public IRepositoryUser Users { get => _users; }
    }
}
