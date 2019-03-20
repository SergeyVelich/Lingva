using Lingva.DAL.Context;
using Lingva.DAL.Repositories.Contracts;
using Lingva.DAL.UnitsOfWork.Contracts;
using System;
using System.Threading.Tasks;

namespace Lingva.DAL.UnitsOfWork
{
    public class UnitOfWorkUser : UnitOfWork, IUnitOfWorkUser
    {
        private readonly IRepositoryUser _users;

        public UnitOfWorkUser(DictionaryContext context, IRepositoryUser users) : base(context)
        {
            _users = users;
        }

        public IRepositoryUser Users { get => _users; }
    }
}
