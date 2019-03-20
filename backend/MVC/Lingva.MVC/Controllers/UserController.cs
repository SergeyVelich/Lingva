using Lingva.BC.Contracts;
using Lingva.BC.DTO;
using Lingva.Common.Mapping;
using Lingva.WebAPI.ViewModel.Request;
using Lingva.WebAPI.ViewModel.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.MVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IDataAdapter _dataAdapter;

        public UserController(IUserService userService, IDataAdapter dataAdapter)
        {
            _userService = userService;
            _dataAdapter = dataAdapter;
        }

        // GET: 
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetListAsync();

            return View(_dataAdapter.Map<IEnumerable<UserViewModel>>(users));
        }

        // GET: 
        [HttpGet]
        public async Task<IActionResult> GetProfile(int id)
        {
            UserDTO userDTO = await _userService.GetByIdAsync(id);

            if (userDTO == null)
            {
                return NotFound();
            }

            return View(_dataAdapter.Map<UserViewModel>(userDTO));
        }

        // GET: User/Update?id=2
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            UserDTO userDTO = await _userService.GetByIdAsync(id);

            if (userDTO == null)
            {
                return NotFound();
            }

            return View(_dataAdapter.Map<UserCreateViewModel>(userDTO));
        }

        // POST: group/Update
        [HttpPost]
        public async Task<IActionResult> Update(UserCreateViewModel userCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                UserDTO userDTO = _dataAdapter.Map<UserDTO>(userCreateViewModel);
                await _userService.UpdateAsync(userDTO.Id, userDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Redirect("/User/Index");
        }
    }
}
