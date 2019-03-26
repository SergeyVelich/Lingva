using Lingva.BC.Contracts;
using Lingva.BC.DTO;
using Lingva.Common.Mapping;
using Lingva.WebAPI.ViewModel.Request;
using Lingva.WebAPI.ViewModel.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.WebAPI.Controllers
{
    //[Authorize]
    [Route("api/group")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IDataAdapter _dataAdapter;
        private readonly ILogger<GroupController> _logger;

        public GroupController(IGroupService groupService, IDataAdapter dataAdapter, ILogger<GroupController> logger)
        {
            _groupService = groupService;
            _dataAdapter = dataAdapter;
            _logger = logger;
        }

        // GET: api/group
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<GroupDTO> groups = await _groupService.GetListAsync();

            return Ok(_dataAdapter.Map<IEnumerable<GroupViewModel>>(groups));
        }

        // GET: api/group/get?id=2
        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GroupDTO group = await _groupService.GetByIdAsync(id);

            if (group == null)
            {
                return NotFound();
            }

            return Ok(_dataAdapter.Map<GroupViewModel>(group));
        }

        // POST: api/group/create
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] GroupCreateViewModel groupCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                GroupDTO group = _dataAdapter.Map<GroupDTO>(groupCreateViewModel);
                await _groupService.AddAsync(group);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        // PUT: api/group/update
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] GroupCreateViewModel groupCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                GroupDTO group = _dataAdapter.Map<GroupDTO>(groupCreateViewModel);
                await _groupService.UpdateAsync(group.Id, group);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        // DELETE: api/group/delete?id=2
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _groupService.DeleteAsync(id);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}