using Lingva.BC.Contracts;
using Lingva.BC.DTO;
using Lingva.Common.Mapping;
using Lingva.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Lingva.BC.Services
{
    public class AccountService : IAccountService
    {
        private readonly IDataAdapter _dataAdapter;
        private readonly UserManager<Account> _accountManager;
        private readonly SignInManager<Account> _signInManager;

        public AccountService(UserManager<Account> accountManager, SignInManager<Account> signInManager, IDataAdapter dataAdapter)
        {
            _accountManager = accountManager;
            _signInManager = signInManager;
            _dataAdapter = dataAdapter;
        }

        public async Task<bool> Register(AccountDTO accountDTO, string password)
        {
            Account account = _dataAdapter.Map<Account>(accountDTO);
            var result = await _accountManager.CreateAsync(account, password);
            if (!result.Succeeded)
            {
                await _signInManager.SignInAsync(account, false);
            }

            return result.Succeeded;
        }

        public async Task<bool> Login(string email, string password, bool rememberMe, bool lockoutOnFailure = false)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure);

            return result.Succeeded;
        }

        public async Task LogOff()
        {           
            await _signInManager.SignOutAsync(); // удаляем аутентификационные куки
        }
    }
}
