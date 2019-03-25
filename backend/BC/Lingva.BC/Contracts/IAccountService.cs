using Lingva.BC.DTO;
using System.Threading.Tasks;

namespace Lingva.BC.Contracts
{
    public interface IAccountService
    {
        Task<bool> Register(AccountDTO accountDTO, string password);
        Task<bool> Login(string email, string password, bool rememberMe, bool lockoutOnFailure = false);
        Task LogOff();
    }
}
