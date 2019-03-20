using Lingva.BC.Auth;
using Lingva.Common.Mapping;
using Lingva.MVC.ViewModel.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lingva.MVC.Controllers
{
    [Authorize]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IDataAdapter _dataAdapter;

        public AuthController(IAuthService authService, IDataAdapter dataAdapter)
        {
            _authService = authService;
            _dataAdapter = dataAdapter;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]       
        public IActionResult Authenticate(AuthRequestViewModel authRequestViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AuthRequest authRequest = _dataAdapter.Map<AuthRequest>(authRequestViewModel);
            JwtToken token = _authService.Authenticate(authRequest);

            if (token == null)
            {
                return BadRequest("Username or password is incorrect");
            }

            return View("~/Views/Home/Index.cshtml");
        }
    }
}
