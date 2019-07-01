using Lingva.BC.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.BC.Contracts
{
    public interface IUserManager
    {
        Task<IEnumerable<UserDto>> GetListByGroupAsync(int id);
    }
}
