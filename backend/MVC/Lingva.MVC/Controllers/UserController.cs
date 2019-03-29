using Lingva.Common.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Lingva.MVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        //private readonly IDataAdapter _dataAdapter;
        //private readonly ILogger<UserController> _logger;

        //public UserController(IDataAdapter dataAdapter, ILogger<UserController> logger)
        //{
        //    _dataAdapter = dataAdapter;
        //    _logger = logger;
        //}

        //// GET: 
        //public async Task<IActionResult> Index()
        //{
        //    var users = await _userService.GetListAsync();

        //    return View(_dataAdapter.Map<IEnumerable<UserViewModel>>(users));
        //}

        //// GET: 
        //[HttpGet]
        //public async Task<IActionResult> Get(int id)
        //{
        //    UserDTO userDTO = await _userService.GetByIdAsync(id);

        //    if (userDTO == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(_dataAdapter.Map<UserViewModel>(userDTO));
        //}

        //// GET: user/Create
        //[HttpGet]
        //[Route("registration")]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: user/Create
        //[HttpPost]
        //public async Task<IActionResult> Create(UserCreateViewModel userCreateViewModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        UserDTO userDTO = _dataAdapter.Map<UserDTO>(userCreateViewModel);
        //        await _userService.AddAsync(userDTO);
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //    return Redirect("/User/Index");
        //}

        //// GET: user/Update?id=2
        //[HttpGet]
        //public async Task<IActionResult> Update(int id)
        //{
        //    throw new NotImplementedException();

        //    //UserDTO userDTO = await _userService.GetByIdAsync(id);

        //    //if (userDTO == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    //return View(_dataAdapter.Map<UserCreateViewModel>(userDTO));
        //}

        //// POST: user/Update
        //[HttpPost]
        //public async Task<IActionResult> Update(UserCreateViewModel userCreateViewModel)
        //{
        //    throw new NotImplementedException();

        //    //if (!ModelState.IsValid)
        //    //{
        //    //    return BadRequest(ModelState);
        //    //}

        //    //try
        //    //{
        //    //    UserDTO userDTO = _dataAdapter.Map<UserDTO>(userCreateViewModel);
        //    //    await _userService.UpdateAsync(userDTO.Id, userDTO);
        //    //}
        //    //catch (ArgumentException ex)
        //    //{
        //    //    return BadRequest(ex.Message);
        //    //}

        //    //return Redirect("/User/Index");
        //}
    }
}
