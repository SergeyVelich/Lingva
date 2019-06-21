using Lingva.Additional.Mapping.DataAdapter;
using Lingva.BC.Contracts;
using Lingva.BC.Dto;
using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.BC.Services
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IDataAdapter _dataAdapter;
           
        public UserManager(IUserRepository userRepository, IDataAdapter dataAdapter)
        {
            _userRepository = userRepository;
            _dataAdapter = dataAdapter;
        }

        public async Task<IEnumerable<UserDto>> GetListByGroupAsync(int id)
        {
            IEnumerable<User> users = await _userRepository.GetListByGroupAsync(id);

            return _dataAdapter.Map<IEnumerable<UserDto>>(users);
        }      
    }
}

