using Lingva.BC.Contracts;
using Lingva.BC.DTO;
using Lingva.Common.Mapping;
using Lingva.MVC.ViewModel.Request;
using Lingva.MVC.ViewModel.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.MVC.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IDataAdapter _dataAdapter;

        public GroupController(IGroupService groupService, IDataAdapter dataAdapter)
        {
            _groupService = groupService;
            _dataAdapter = dataAdapter;
        }

        // GET: group
        public async Task<IActionResult> Index()
        {
            var groups = await _groupService.GetGroupsListAsync();

            return View(_dataAdapter.Map<IEnumerable<GroupViewModel>>(groups));
        }

        // GET: group/get?id=2
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            GroupDTO group = await _groupService.GetGroupAsync(id);

            if (group == null)
            {
                return NotFound();
            }

            return View(_dataAdapter.Map<GroupViewModel>(group));
        }

        // GET: group/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: group/Create
        [HttpPost]
        public async Task<IActionResult> Create(GroupCreateViewModel groupCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                GroupDTO group = _dataAdapter.Map<GroupDTO>(groupCreateViewModel);
                await _groupService.AddGroupAsync(group);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Redirect("/Group/Index");
        }

        // GET: group/Update?id=2
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            GroupDTO group = await _groupService.GetGroupAsync(id);

            if (group == null)
            {
                return NotFound();
            }

            return View(_dataAdapter.Map<GroupCreateViewModel>(group));
        }

        // POST: group/Update
        [HttpPost]
        public async Task<IActionResult> Update(GroupCreateViewModel groupCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                GroupDTO groupDTO = _dataAdapter.Map<GroupDTO>(groupCreateViewModel);
                await _groupService.UpdateGroupAsync(groupDTO.Id, groupDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Redirect("/Group/Index");
        }

        // POST: groups/Delete?id=2
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _groupService.DeleteGroupAsync(id);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Redirect("/Group/Index");
        }
    }
}
