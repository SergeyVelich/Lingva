using Lingva.BC.Contracts;
using Lingva.BC.DTO;
using Lingva.Common.Mapping;
using Lingva.WebAPI.ViewModel.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Lingva.WebAPI.Controllers
{
    [Authorize]
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IDataAdapter _dataAdapter;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService accountService, IDataAdapter dataAdapter, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _dataAdapter = dataAdapter;
            _logger = logger;
        }

        // POST: api/account/register
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AccountDTO accountDTO = _dataAdapter.Map<AccountDTO>(registerViewModel);
            if(!await _accountService.Register(accountDTO, registerViewModel.Password))
            {
                return BadRequest();
            }

            return Ok();
        }

        // POST: api/account/login
        [AllowAnonymous]
        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody]LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AccountDTO accountDTO = _dataAdapter.Map<AccountDTO>(loginViewModel);

            if (!await _accountService.Login(accountDTO.Email, loginViewModel.Password, loginViewModel.RememberMe))
            {
                return BadRequest("Account name or password is incorrect");
            }

            return Ok();
        }

        // POST: api/account/logoff
        [HttpPost("logoff")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _accountService.LogOff(); // удаляем аутентификационные куки

            return Ok();
        }
    }
}