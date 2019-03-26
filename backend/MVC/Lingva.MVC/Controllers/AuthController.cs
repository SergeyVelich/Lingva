using Lingva.BC.Auth;
using Lingva.Common.Mapping;
using Lingva.MVC.ViewModel.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Lingva.MVC.Controllers
{
    //[Authorize]
    //public class AuthController : Controller
    //{
    //    private readonly IAuthService _authService;
    //    private readonly IDataAdapter _dataAdapter;
    //    private readonly ILogger<AuthController> _logger;

    //    public AuthController(IAuthService authService, IDataAdapter dataAdapter, ILogger<AuthController> logger)
    //    {
    //        _authService = authService;
    //        _dataAdapter = dataAdapter;
    //        _logger = logger;
    //    }

    //    [AllowAnonymous]
    //    [HttpGet]
    //    public IActionResult Index()
    //    {
    //        return View();
    //    }

    //    [AllowAnonymous]
    //    [HttpPost]       
    //    public IActionResult Authenticate(AuthRequestViewModel authRequestViewModel)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }

    //        AuthRequest authRequest = _dataAdapter.Map<AuthRequest>(authRequestViewModel);
    //        string token = _authService.Authenticate(authRequest);

    //        if (token == null)
    //        {
    //            return BadRequest("Username or password is incorrect");
    //        }

    //        return View("~/Views/Home/Index.cshtml");
    //    }
    //}
}
