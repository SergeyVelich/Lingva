using Lingva.BC.Contracts;
using Lingva.BC.Dto;
using Lingva.Common.Mapping;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.BC.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IDataAdapter _dataAdapter;
           
        public UserService(IUserRepository repository, IDataAdapter dataAdapter)
        {
            _repository = repository;
            _dataAdapter = dataAdapter;
        }

        public async Task<IEnumerable<UserDto>> GetListByGroupAsync(int id)
        {
            IEnumerable<User> users = await _repository.GetListByGroupAsync(id);

            return _dataAdapter.Map<IEnumerable<UserDto>>(users);
        }      
    }
}

