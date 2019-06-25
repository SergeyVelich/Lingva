using Lingva.Additional.Mapping.DataAdapter;
using Lingva.ASP.Infrastructure.Adapters;
using Lingva.ASP.Infrastructure.Models;
using Lingva.BC.Contracts;
using Lingva.BC.Dto;
using Lingva.MVC.Models.Entities;
using Lingva.MVC.Models.Group;
using Lingva.MVC.Models.Group.Index;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Lingva.MVC.Controllers
{
    //[Authorize]
    [ResponseCache(CacheProfileName = "NoCashing")]
    public class GroupController : Controller
    {
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IGroupManager _groupManager;
        private readonly IInfoManager _infoManager;
        private readonly IFileStorageManager _fileStorageManager;
        private readonly IDataAdapter _dataAdapter;       
        private readonly QueryOptionsAdapter _queryOptionsAdapter;
        private readonly ILogger<GroupController> _logger;    
        private readonly IMemoryCache _memoryCache;

        public GroupController(IHostingEnvironment appEnvironment, IGroupManager groupManager, IInfoManager infoManager, IFileStorageManager fileStorageManager, IDataAdapter dataAdapter, QueryOptionsAdapter queryOptionsAdapter, ILogger<GroupController> logger, IMemoryCache memoryCache)
        {
            _appEnvironment = appEnvironment;
            _groupManager = groupManager;
            _infoManager = infoManager;
            _fileStorageManager = fileStorageManager;
            _dataAdapter = dataAdapter;
            _queryOptionsAdapter = queryOptionsAdapter;
            _logger = logger;                
            _memoryCache = memoryCache;            
        }

        // GET: group    
        public async Task<IActionResult> Index(GroupsListOptionsModel modelOptions)
        {
            IEnumerable<GroupViewModel> groups = await GetGroupsCollectionAsync(modelOptions);
            IList<LanguageViewModel> languages = await GetLanguagesCollectionAsync();
            int pageTotal = await _groupManager.CountAsync(_queryOptionsAdapter.Map(modelOptions));

            GroupsListPageViewModel viewModel = new GroupsListPageViewModel
            {
                PagenatorViewModel = new PagenatorViewModel(pageTotal, modelOptions.PageIndex, modelOptions.PageSize),
                SortViewModel = new SortViewModel(modelOptions.SortProperty, modelOptions.SortOrder),
                FilterViewModel = new FilterViewModel(languages, modelOptions.Name, modelOptions.LanguageId, modelOptions.Description, modelOptions.DateFrom, modelOptions.DateTo),
                Groups = groups,
            };
            
            return View(viewModel);
        }

        // GET: group/get?id=2
        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GroupDto groupDto = await _groupManager.GetByIdAsync((int)id);
            GroupViewModel groupViewModel = _dataAdapter.Map<GroupViewModel>(groupDto);
            GroupPageViewModel viewModel = new GroupPageViewModel(groupViewModel);

            return View(viewModel);
        }

        // GET: group/create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            IList<LanguageViewModel> languages = await GetLanguagesCollectionAsync();
            GroupPageViewModel viewModel = new GroupPageViewModel(new GroupViewModel(), languages);

            return View(viewModel);
        }

        // POST: group/create
        [HttpPost]
        public async Task<IActionResult> Create(GroupViewModel groupViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // сохраняем файл в папку Files в каталоге wwwroot
            string path = "/files/" + groupViewModel.ImageFile.FileName;
            await _fileStorageManager.SaveFileAsync(groupViewModel.ImageFile, _appEnvironment.WebRootPath + path, FileMode.Create);
            groupViewModel.ImagePath = path;

            GroupDto groupDto = _dataAdapter.Map<GroupDto>(groupViewModel);
            await _groupManager.AddAsync(groupDto);

            return RedirectToAction("Index");
        }

        // GET: group/update?id=2
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GroupDto groupDto = await _groupManager.GetByIdAsync((int)id);
            if (groupDto == null)
            {
                return NotFound();
            }

            GroupViewModel groupViewModel = _dataAdapter.Map<GroupViewModel>(groupDto);
            IList<LanguageViewModel> languages = await GetLanguagesCollectionAsync();
            GroupPageViewModel viewModel = new GroupPageViewModel(groupViewModel, languages);

            return View(viewModel);
        }

        // POST: group/update
        [HttpPost]
        public async Task<IActionResult> Update(GroupViewModel groupViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // сохраняем файл в папку Files в каталоге wwwroot
            string path = "/files/" + groupViewModel.ImageFile.FileName;            
            await _fileStorageManager.SaveFileAsync(groupViewModel.ImageFile, _appEnvironment.WebRootPath + path, FileMode.Create);
            groupViewModel.ImagePath = path;

            GroupDto groupDto = _dataAdapter.Map<GroupDto>(groupViewModel);
            await _groupManager.UpdateAsync(groupDto);

            return RedirectToAction("Index");
        }

        // GET: group/delete?id=2
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GroupDto groupDto = await _groupManager.GetByIdAsync((int)id);
            if (groupDto == null)
            {
                return NotFound();
            }

            GroupViewModel groupViewModel = _dataAdapter.Map<GroupViewModel>(groupDto);
            GroupPageViewModel viewModel = new GroupPageViewModel(groupViewModel);

            return View(viewModel);
        }

        // POST: group/delete
        [HttpPost]
        public async Task<IActionResult> Delete(GroupViewModel groupViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _groupManager.DeleteAsync(groupViewModel.Id);

            return RedirectToAction("Index");
        }

        private async Task<IList<LanguageViewModel>> GetLanguagesCollectionAsync()
        {
            IEnumerable<LanguageDto> languagessDto = await _infoManager.GetLanguagesListAsync();
            IList<LanguageViewModel> languages = _dataAdapter.Map<IList<LanguageViewModel>>(languagessDto);

            return languages;
        }

        private async Task<IEnumerable<GroupViewModel>> GetGroupsCollectionAsync(GroupsListOptionsModel modelOptions)
        {
            IEnumerable<GroupDto> groupsDto = await _groupManager.GetListAsync(_queryOptionsAdapter.Map(modelOptions));
            IEnumerable<GroupViewModel> groups = _dataAdapter.Map<IEnumerable<GroupViewModel>>(groupsDto);

            return groups;
        }
    }
}
