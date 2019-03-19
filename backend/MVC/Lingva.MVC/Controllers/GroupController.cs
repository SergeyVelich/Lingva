using Lingva.BC.Contracts;
using Lingva.BC.DTO;
using Lingva.Common.Mapping;
using Lingva.MVC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.MVC.Controllers
{
    //[Route("groups")]
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
        public IActionResult Index()
        {
            var groups = _groupService.GetGroupsList();

            return View(_dataAdapter.Map<IEnumerable<GroupViewModel>>(groups));
        }

        // GET: group/get?id=2
        [HttpGet]
        public IActionResult Get(int id)
        {
            GroupDTO group = _groupService.GetGroup(id);

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
        public async Task<IActionResult> Create(GroupViewModel groupViewModel)
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

            return Redirect("Index");
        }

        // GET: group/Update?id=2
        [HttpGet]
        public IActionResult Update(int id)
        {
            GroupDTO group = _groupService.GetGroup(id);

            if (group == null)
            {
                return NotFound();
            }

            return View(_dataAdapter.Map<GroupViewModel>(group));
        }

        // POST: group/Update
        [HttpPost]
        public async Task<IActionResult> Update(GroupViewModel groupViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                GroupDTO groupDTO = _dataAdapter.Map<GroupDTO>(groupViewModel);
                await Task.Run(() => _groupService.UpdateGroup(groupDTO.Id, groupDTO));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Redirect("Index");
        }

        // POST: groups/Delete/1
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
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

            return Redirect("Index");
        }
    }
}
