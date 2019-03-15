using AutoMapper;
using Lingva.BusinessLayer.Contracts;
using Lingva.DataAccessLayer.Entities;
using Lingva.MVC.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.MVC.Controllers
{
    [Route("groups")]
    public class GroupManagementController : Controller
    {
        private readonly IGroupManagementService _groupManagementService;
        private readonly IMapper _mapper;

        public GroupManagementController(IGroupManagementService groupManagementService, IMapper mapper)
        {
            _groupManagementService = groupManagementService;
            _mapper = mapper;
        }

        // GET: groups
        public IActionResult Index()
        {
            var groups = _groupManagementService.GetGroupsList();

            return View(_mapper.Map<IEnumerable<GroupDTO>>(groups));
        }

        // GET: groups/getGroup/5
        [HttpGet("getGroup")]
        public IActionResult GetGroup(int id)
        {
            Group group = _groupManagementService.GetGroup(id);

            if (group == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<GroupDTO>(group));
        }

        // GET: groups/Create
        [HttpGet("Create")]
        public IActionResult CreateGroup()
        {
            ViewBag.AddSubmit = true;
            return View();
        }

        // POST: groups/Create
        [HttpPost("Create")]
        public async Task<IActionResult> CreateGroup(GroupDTO groupDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Group group = _mapper.Map<Group>(groupDTO);
                await Task.Run(() => _groupManagementService.AddGroup(group));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Redirect("/groups/index");
        }

        // GET: groups/Update/1
        [HttpGet("Update/{id?}")]
        public IActionResult UpdateGroup(int id)
        {
            ViewBag.AddSubmit = true;

            Group group = _groupManagementService.GetGroup(id);

            if (group == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<GroupDTO>(group));
        }

        // POST: groups/Update
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateGroup(GroupDTO groupDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Group group = _mapper.Map<Group>(groupDTO);
                await Task.Run(() => _groupManagementService.UpdateGroup(groupDTO.Id, group));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Redirect("/groups/index");
        }

        // POST: groups/Delete/1
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
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

            return Redirect("/groups/index");
        }
    }
}
