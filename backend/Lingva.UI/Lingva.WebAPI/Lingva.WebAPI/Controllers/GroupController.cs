using Lingva.Additional.Mapping.DataAdapter;
using Lingva.ASP.Infrastructure.Adapters;
using Lingva.ASP.Infrastructure.Models;
using Lingva.BC.Contracts;
using Lingva.BC.Dto;
using Lingva.WebAPI.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.WebAPI.Controllers
{
    [Authorize(Policy = "ApiReader")]
    [Route("api/group")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupManager _groupManager;
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IDataAdapter _dataAdapter;
        private readonly QueryOptionsAdapter _queryOptionsAdapter;
        private readonly ILogger<GroupController> _logger;        

        public GroupController(IGroupManager groupManager, IFileStorageManager fileStorageManager, IDataAdapter dataAdapter, QueryOptionsAdapter queryOptionsAdapter, ILogger<GroupController> logger)
        {
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
        public async Task<IActionResult> Create()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GroupViewModel groupViewModel = GetGroupViewModelFromRequest();

            GroupDto groupDto = _dataAdapter.Map<GroupDto>(groupViewModel);
            await _groupManager.AddAsync(groupDto);

            await SaveFileFromRequestAsync(groupDto.Id);

            return CreatedAtAction("Get", new { id = groupDto.Id }, _dataAdapter.Map<GroupViewModel>(groupDto));
        }

        // PUT: api/group/update
        [HttpPut("update")]
        public async Task<IActionResult> Update()
        {           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GroupViewModel groupViewModel = GetGroupViewModelFromRequest();

            GroupDto groupDto = _dataAdapter.Map<GroupDto>(groupViewModel);
            await _groupManager.UpdateAsync(groupDto);

            await SaveFileFromRequestAsync(groupDto.Id);         

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

        // GET: api/group/getImage?fileId=1
        [AllowAnonymous]
        [HttpGet("getImage")]
        public async Task<IActionResult> GetImage([FromQuery] int id)
        {
            string folderName = Path.Combine("Resources", "Images");
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            DirectoryInfo directory = new DirectoryInfo(directoryPath);
            FileInfo[] files = directory.GetFiles(id.ToString() + ".*");
            string path = files.Select(file => file.FullName).FirstOrDefault();
            FileStream image = System.IO.File.OpenRead(path);
            
            return File(image, "image/jpeg");
        }

        private GroupViewModel GetGroupViewModelFromRequest()
        {
            GroupViewModel groupViewModel = null;
            var stringItems = Request.Form["group"];
            foreach (var stringItem in stringItems)
            {
                groupViewModel = JsonConvert.DeserializeObject<GroupViewModel>(stringItem);
            }

            return groupViewModel;
        }
        private async Task<string> SaveFileFromRequestAsync(int id)
        {
            string path = null;
            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files[0];
                string folderName = Path.Combine("Resources", "Images");
                path = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), folderName), id.ToString());
                await _fileStorageManager.SaveFileAsync(file, path, FileMode.Create);
            }

            return path;
        }
    }
}