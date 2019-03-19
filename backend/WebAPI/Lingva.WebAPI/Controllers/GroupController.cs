using Lingva.BC.Contracts;
using Lingva.BC.DTO;
using Lingva.Common.Mapping;
using Lingva.WebAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.WebAPI.Controllers
{
    [Route("api/groups")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IDataAdapter _dataAdapter;

        public GroupController(IGroupService groupService, IDataAdapter dataAdapter)
        {
            _groupService = groupService;
            _dataAdapter = dataAdapter;
        }

        // GET: api/groups
        [HttpGet]
        public async Task<IActionResult> GetGroupsList()
        {
            var groups = _groupService.GetGroupsList();

            return Ok(_dataAdapter.Map<IEnumerable<GroupViewModel>>(groups));
        }

        // GET: api/groups/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroup([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GroupDTO group = _groupService.GetGroup(id);

            if (group == null)
            {
                return NotFound();
            }

            return Ok(_dataAdapter.Map<GroupViewModel>(group));
        }

        // POST: api/groups
        [HttpPost]
        public async Task<IActionResult> PostGroup([FromBody] GroupViewModel groupViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                GroupDTO group = _dataAdapter.Map<GroupDTO>(groupViewModel);
                await Task.Run(() => _groupService.AddGroup(group));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        // PUT: api/groups
        [HttpPut]
        public async Task<IActionResult> PutGroup([FromBody] GroupViewModel groupViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                GroupDTO group = _dataAdapter.Map<GroupDTO>(groupViewModel);
                await Task.Run(() => _groupService.UpdateGroup(group.Id, group));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        // DELETE: api/groups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await Task.Run(() => _groupService.DeleteGroup(id));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}