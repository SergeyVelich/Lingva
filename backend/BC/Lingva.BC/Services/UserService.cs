using Lingva.BC.Auth;
using Lingva.BC.Contracts;
using Lingva.BC.Crypto;
using Lingva.BC.DTO;
using Lingva.Common.Mapping;
using Lingva.DAL.Entities;
using Lingva.DAL.UnitsOfWork.Contracts;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.BC.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWorkUser _unitOfWork;
        private readonly IDataAdapter _dataAdapter;
        private readonly IDefaultCryptoProvider _defaultCryptoProvider;

        public UserService(IUnitOfWorkUser unitOfWork, IDataAdapter dataAdapter, IDefaultCryptoProvider defaultCryptoProvider)
        {
            _unitOfWork = unitOfWork;
            _dataAdapter = dataAdapter;
            _defaultCryptoProvider = defaultCryptoProvider;
        }

        public async Task<IEnumerable<UserDTO>> GetListAsync()
        {
            IEnumerable<User> users = await _unitOfWork.Users.GetListAsync();
            return _dataAdapter.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> GetByIdAsync(int id)
        {
            User user = await _unitOfWork.Users.GetByIdAsync(id);
            return _dataAdapter.Map<UserDTO>(user);
        }

        public async Task<UserDTO> AddAsync(UserDTO userDTO)
        {
            User user = _dataAdapter.Map<User>(userDTO);
            user.PasswordHash = _defaultCryptoProvider.GetHashHMACSHA512(userDTO.Password, out byte[] salt);
            user.Salt = salt;
            _unitOfWork.Users.Create(user);
            await _unitOfWork.SaveAsync();

            return _dataAdapter.Map<UserDTO>(user);
        }

        public async Task<UserDTO> UpdateAsync(int id, UserDTO updateUserDTO)
        {
            User currentUser = await _unitOfWork.Users.GetByIdAsync(id);
            User updateUser = _dataAdapter.Map<User>(updateUserDTO);
            _dataAdapter.Update<User>(updateUser, currentUser);
            _unitOfWork.Users.Update(currentUser);
            await _unitOfWork.SaveAsync();

            return _dataAdapter.Map<UserDTO>(currentUser);
        }

        public async Task DeleteAsync(int id)
        {
            User user = await _unitOfWork.Users.GetByIdAsync(id);

            if (user == null)
            {
                return;
            }

            _unitOfWork.Users.Delete(user);
            await _unitOfWork.SaveAsync();
        }
    }
}
