using Lingva.BC.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lingva.BC.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetListAsync();

        Task<UserDTO> GetByIdAsync(int id);

        Task<UserDTO> AddAsync(UserDTO group);

        Task<UserDTO> UpdateAsync(int id, UserDTO group);

        Task DeleteAsync(int id);
    }
}
