﻿using Lingva.BC.Contracts;
using Lingva.BC.DTO;
using Lingva.Common.Mapping;
using Lingva.WebAPI.ViewModel.Request;
using Lingva.WebAPI.ViewModel.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public UserController(IUserService userService, IDataAdapter dataAdapter)
        {
            _userService = userService;
            _dataAdapter = dataAdapter;
        }

        // GET: api/user
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetListAsync();

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

            UserDTO user = await _userService.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(_dataAdapter.Map<UserViewModel>(user));
        }

        // POST: api/user/create
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UserCreateViewModel userCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                UserDTO user = _dataAdapter.Map<UserDTO>(userCreateViewModel);
                await _userService.AddAsync(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        // PUT: api/user/update
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UserCreateViewModel userCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                UserDTO user = _dataAdapter.Map<UserDTO>(userCreateViewModel);
                await _userService.UpdateAsync(user.Id, user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        // DELETE: api/user?id=2
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _userService.DeleteAsync(id);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}