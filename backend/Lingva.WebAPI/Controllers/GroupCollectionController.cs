using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Lingva.DataAccessLayer.Entities;
using Lingva.BusinessLayer.Contracts;
using Lingva.WebAPI.Dto;

namespace Lingva.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupCollectionController : ControllerBase
    {
        private readonly IGroupsCollectionService _groupsCollectionService;
        private readonly IMapper _mapper;

        public GroupCollectionController(IGroupsCollectionService groupsCollectionService, IMapper mapper)
        {
            _groupsCollectionService = groupsCollectionService;
            _mapper = mapper;
        }

        // GET: api/event
        [HttpGet]
        public async Task<IActionResult> GetGroupsList()
        {
            var groups = _groupsCollectionService.GetGroupsList();

            return Ok(_mapper.Map<IEnumerable<GroupViewDTO>>(groups));
        }

        // GET: api/event/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroup([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Group group = _groupsCollectionService.GetGroup(id);

            if (group == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GroupViewDTO>(group));
        }

        // POST: api/event
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
                await Task.Run(() => _groupsCollectionService.AddGroup(group));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        // PUT: api/event/5
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
                await Task.Run(() => _groupsCollectionService.UpdateGroup(id, group));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        // DELETE: api/event/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await Task.Run(() => _groupsCollectionService.DeleteGroup(id));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}