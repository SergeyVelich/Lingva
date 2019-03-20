using Lingva.BC.Contracts;
using Lingva.BC.DTO;
using Lingva.Common.Mapping;
using Lingva.MVC.ViewModel.Request;
using Lingva.MVC.ViewModel.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.MVC.Controllers
{
    [Authorize]
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
            var groups = await _groupService.GetListAsync();

            return View(_dataAdapter.Map<IEnumerable<GroupViewModel>>(groups));
        }

        // GET: group/get?id=2
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            GroupDTO groupDTO = await _groupService.GetByIdAsync(id);

            if (groupDTO == null)
            {
                return NotFound();
            }

            return View(_dataAdapter.Map<GroupViewModel>(groupDTO));
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
                GroupDTO groupDTO = _dataAdapter.Map<GroupDTO>(groupCreateViewModel);
                await _groupService.AddAsync(groupDTO);
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
            GroupDTO groupDTO = await _groupService.GetByIdAsync(id);

            if (groupDTO == null)
            {
                return NotFound();
            }

            return View(_dataAdapter.Map<GroupCreateViewModel>(groupDTO));
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
                await _groupService.UpdateAsync(groupDTO.Id, groupDTO);
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
                await _groupService.DeleteAsync(id);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Redirect("/Group/Index");
        }
    }
}
