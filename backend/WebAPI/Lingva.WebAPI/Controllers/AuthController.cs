using Lingva.BC.Auth;
using Lingva.Common.Mapping;
using Lingva.WebAPI.ViewModel.Request;
using Lingva.WebAPI.ViewModel.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Lingva.WebAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IDataAdapter _dataAdapter;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, IDataAdapter dataAdapter, ILogger<AuthController> logger)
        {
            _authService = authService;
            _dataAdapter = dataAdapter;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]       
        public IActionResult Authenticate([FromBody]AuthRequestViewModel authRequestViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AuthRequest authRequest = _dataAdapter.Map<AuthRequest>(authRequestViewModel);
            string token = _authService.Authenticate(authRequest);

            if (token == null)
            {
                return BadRequest("Username or password is incorrect");
            }

            return Ok(token);
        }
    }
}
