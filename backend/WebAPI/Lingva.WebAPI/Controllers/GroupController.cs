﻿using Lingva.BC.Common.Enums;
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
using System.Linq;
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
        public async Task<IActionResult> Index([FromQuery] FilterViewModel filters, [FromQuery] SorterViewModel sorters, int page = 1)
        {
            int pageSize = 3;

            IEnumerable<GroupDTO> groupsDto = await _groupService.GetListAsync();

            if (filters.Language != 0)
            {
                groupsDto = groupsDto.Where(g => g.LanguageId == filters.Language).ToList();
            }

            if (!String.IsNullOrEmpty(filters.Name))
            {
                groupsDto = groupsDto.Where(g => g.Name.Contains(filters.Name, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            //switch (sortOrder)
            //{
            //    case SortState.NameDesc:
            //        groupsDto = groupsDto.OrderByDescending(s => s.Name);
            //        break;
            //    case SortState.DateAsc:
            //        groupsDto = groupsDto.OrderBy(s => s.Description);
            //        break;
            //    case SortState.DateDesc:
            //        groupsDto = groupsDto.OrderByDescending(s => s.Description);
            //        break;
            //    case SortState.LanguageAsc:
            //        groupsDto = groupsDto.OrderBy(s => s.Picture);
            //        break;
            //    case SortState.LanguageDesc:
            //        groupsDto = groupsDto.OrderByDescending(s => s.Picture);
            //        break;
            //    default:
            //        groupsDto = groupsDto.OrderBy(s => s.Name);
            //        break;
            //}

            var count = groupsDto.Count();
            groupsDto = groupsDto.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return Ok(_dataAdapter.Map<IEnumerable<GroupViewModel>>(groupsDto));
        }

        // GET: api/group/get?id=2
        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GroupDTO groupDto = await _groupService.GetByIdAsync(id);

            if (groupDto == null)
            {
                return NotFound();
            }

            return Ok(_dataAdapter.Map<GroupViewModel>(groupDto));
        }

        // POST: api/group/create
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] GroupCreateViewModel groupCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GroupDTO groupDto = _dataAdapter.Map<GroupDTO>(groupCreateViewModel);
            await _groupService.AddAsync(groupDto);

            return Ok(_dataAdapter.Map<GroupViewModel>(groupDto));
        }

        // PUT: api/group/update
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] GroupCreateViewModel groupCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GroupDTO groupDto = _dataAdapter.Map<GroupDTO>(groupCreateViewModel);
            await _groupService.UpdateAsync(groupDto.Id, groupDto);

            return Ok(_dataAdapter.Map<GroupViewModel>(groupDto));
        }

        // DELETE: api/group/delete
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] GroupCreateViewModel groupCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GroupDTO groupDto = _dataAdapter.Map<GroupDTO>(groupCreateViewModel);
            await _groupService.DeleteAsync(groupDto);

            return Ok(_dataAdapter.Map<GroupViewModel>(groupDto));
        }
    }
}