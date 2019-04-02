using Lingva.BC.Contracts;
using Lingva.BC.DTO;
using Lingva.Common.Mapping;
using Lingva.WebAPI.Models.Request;
using Lingva.WebAPI.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.WebAPI.Controllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IDataAdapter _dataAdapter;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, IDataAdapter dataAdapter, ILogger<UserController> logger)
        {
            _userService = userService;
            _dataAdapter = dataAdapter;
            _logger = logger;
        }

        // GET: api/user
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<UserDTO> users = await _userService.GetListAsync();

            return Ok(_dataAdapter.Map<IEnumerable<UserViewModel>>(users));
        }

        // GET: api/user?id=2
        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserDTO userDto = await _userService.GetByIdAsync(id);

            if (userDto == null)
            {
                return NotFound();
            }

            return Ok(_dataAdapter.Map<UserViewModel>(userDto));
        }

        // POST: api/user/create
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UserCreateViewModel userCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserDTO userDto = _dataAdapter.Map<UserDTO>(userCreateViewModel);
            await _userService.AddAsync(userDto);

            return Ok(userDto);
        }

        // PUT: api/user/update
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UserCreateViewModel userCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserDTO userDto = _dataAdapter.Map<UserDTO>(userCreateViewModel);
            await _userService.UpdateAsync(userDto.Id, userDto);

            return Ok(userDto);
        }
    }
}