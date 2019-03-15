using AutoMapper;
using Lingva.BC.Contracts;
using Lingva.DAL.Entities;
using Lingva.WebAPI.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.WebAPI.Controllers
{
    [Route("api/groups")]
    [ApiController]
    public class GroupManagementController : ControllerBase
    {
        private readonly IGroupManagementService _groupManagementService;
        private readonly IMapper _mapper;

        public GroupManagementController(IGroupManagementService groupManagementService, IMapper mapper)
        {
            _groupManagementService = groupManagementService;
            _mapper = mapper;
        }

        // GET: api/groups
        [HttpGet]
        public async Task<IActionResult> GetGroupsList()
        {
            var groups = _groupManagementService.GetGroupsList();

            return Ok(_mapper.Map<IEnumerable<GroupViewDTO>>(groups));
        }

        // GET: api/groups/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroup([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Group group = _groupManagementService.GetGroup(id);

            if (group == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GroupViewDTO>(group));
        }

        // POST: api/groups
        [HttpPost]
        public async Task<IActionResult> PostGroup([FromBody] GroupCreatingDTO groupCreatingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Group group = _mapper.Map<Group>(groupCreatingDTO);
                await Task.Run(() => _groupManagementService.AddGroup(group));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        // PUT: api/groups/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup([FromRoute] int id, [FromBody] GroupCreatingDTO groupCreatingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Group group = _mapper.Map<Group>(groupCreatingDTO);
                await Task.Run(() => _groupManagementService.UpdateGroup(id, group));
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
                await Task.Run(() => _groupManagementService.DeleteGroup(id));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}