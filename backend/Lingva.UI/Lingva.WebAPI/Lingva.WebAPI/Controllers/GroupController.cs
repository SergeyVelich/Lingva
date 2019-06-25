using Lingva.Additional.Mapping.DataAdapter;
using Lingva.ASP.Infrastructure.Adapters;
using Lingva.ASP.Infrastructure.Models;
using Lingva.BC.Contracts;
using Lingva.BC.Dto;
using Lingva.WebAPI.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Lingva.WebAPI.Controllers
{
    [Authorize(Policy = "ApiReader")]
    [Route("api/group")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IGroupManager _groupManager;
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IDataAdapter _dataAdapter;
        private readonly QueryOptionsAdapter _queryOptionsAdapter;
        private readonly ILogger<GroupController> _logger;        

        public GroupController(IHostingEnvironment appEnvironment, IGroupManager groupManager, IFileStorageManager fileStorageManager, IDataAdapter dataAdapter, QueryOptionsAdapter queryOptionsAdapter, ILogger<GroupController> logger)
        {
            _appEnvironment = appEnvironment;
            _groupManager = groupManager;
            _fileStorageManager = fileStorageManager;
            _dataAdapter = dataAdapter;
            _queryOptionsAdapter = queryOptionsAdapter;
            _logger = logger;
        }

        // GET: api/group
        //[Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] GroupsListOptionsModel groupsListOptionsModel)
        {
            IEnumerable<GroupDto> groupsDto = await _groupManager.GetListAsync(_queryOptionsAdapter.Map(groupsListOptionsModel));

            return Ok(_dataAdapter.Map<IEnumerable<GroupViewModel>>(groupsDto));
        }

        [HttpGet("count")]
        public async Task<IActionResult> Count([FromQuery] GroupsListOptionsModel groupsListOptionsModel)
        {
            int pageTotal = await _groupManager.CountAsync(_queryOptionsAdapter.Map(groupsListOptionsModel));

            return Ok(pageTotal);
        }

        // GET: api/group/get?id=1
        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            GroupDto groupDto = await _groupManager.GetByIdAsync(id);

            if (groupDto == null)
            {
                return NotFound();
            }

            return Ok(_dataAdapter.Map<GroupViewModel>(groupDto));
        }

        // POST: api/group/create
        /// <summary>
        /// Creates a Group.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "Name": "Harry Potter",
        ///        "Date": 12.10.2019,
        ///        "LanguageId": 2,
        ///        "Description": "3",
        ///        "Picture": "2"
        ///     }
        ///
        /// </remarks>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] GroupViewModel groupViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // сохраняем файл в папку Files в каталоге wwwroot
            //string path = "/files/" + groupViewModel.ImageFile.FileName;
            //await _fileStorageManager.SaveFileAsync(groupViewModel.ImageFile, _appEnvironment.WebRootPath + path, FileMode.Create);
            //groupViewModel.ImagePath = path;

            GroupDto groupDto = _dataAdapter.Map<GroupDto>(groupViewModel);
            await _groupManager.AddAsync(groupDto);

            return CreatedAtAction("Get", new { id = groupDto.Id }, _dataAdapter.Map<GroupViewModel>(groupDto));
        }

        // PUT: api/group/update
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] GroupViewModel groupViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // сохраняем файл в папку Files в каталоге wwwroot
            //string path = "/files/" + groupViewModel.ImageFile.FileName;
            //await _fileStorageManager.SaveFileAsync(groupViewModel.ImageFile, _appEnvironment.WebRootPath + path, FileMode.Create);
            //groupViewModel.ImagePath = path;

            GroupDto groupDto = _dataAdapter.Map<GroupDto>(groupViewModel);
            await _groupManager.UpdateAsync(groupDto);

            return Ok(_dataAdapter.Map<GroupViewModel>(groupDto));
        }

        // DELETE: api/group/delete?id=1
        /// <summary>
        /// Deletes a specific Group.
        /// </summary>
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _groupManager.DeleteAsync(id);

            return Ok();
        }
    }
}