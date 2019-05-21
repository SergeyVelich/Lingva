using Lingva.Additional.Mapping.DataAdapter;
using Lingva.ASP.Infrastructure;
using Lingva.ASP.Infrastructure.Exceptions;
using Lingva.ASP.Infrastructure.Models;
using Lingva.BC.Contracts;
using Lingva.BC.Dto;
using Lingva.MVC.Models.Entities;
using Lingva.MVC.Models.Group;
using Lingva.MVC.Models.Group.Index;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.MVC.Controllers
{
    [Authorize]
    [ResponseCache(CacheProfileName = "NoCashing")]
    public class GroupController : Controller
    {
        private readonly IGroupManager _groupService;
        private readonly IInfoManager _infoService;
        private readonly IDataAdapter _dataAdapter;
        private readonly ILogger<GroupController> _logger;
        private readonly QueryOptionsAdapter _queryOptionsAdapter;
        private readonly IMemoryCache _memoryCache;

        public GroupController(IGroupManager groupService, IInfoManager infoService, IDataAdapter dataAdapter, ILogger<GroupController> logger, IMemoryCache memoryCache, QueryOptionsAdapter queryOptionsAdapter)
        {
            _groupService = groupService;
            _infoService = infoService;
            _dataAdapter = dataAdapter;
            _logger = logger;                
            _memoryCache = memoryCache;
            _queryOptionsAdapter = queryOptionsAdapter;
        }

        // GET: group    
        public async Task<IActionResult> Index(GroupsListOptionsModel modelOptions)
        {
            IEnumerable<GroupViewModel> groups = await GetGroupsCollectionAsync(modelOptions);
            IList<LanguageViewModel> languages = await GetLanguagesCollectionAsync();

            GroupsListPageViewModel viewModel = new GroupsListPageViewModel
            {
                PagenatorViewModel = new PagenatorViewModel(modelOptions.TotalRecords, modelOptions.Page, modelOptions.PageRecords),
                SortViewModel = new SortViewModel(modelOptions.SortProperty, modelOptions.SortOrder),
                FilterViewModel = new FilterViewModel(languages, modelOptions.Name, modelOptions.LanguageId, modelOptions.Description, modelOptions.Date),
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

            GroupDto groupDto = await _groupService.GetByIdAsync((int)id);
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

            GroupDto groupDto = _dataAdapter.Map<GroupDto>(groupViewModel);
            await _groupService.AddAsync(groupDto);

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

            GroupDto groupDto = await _groupService.GetByIdAsync((int)id);
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

            GroupDto groupDto = _dataAdapter.Map<GroupDto>(groupViewModel);
            await _groupService.UpdateAsync(groupDto);

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

            GroupDto groupDto = await _groupService.GetByIdAsync((int)id);
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

            await _groupService.DeleteAsync(groupViewModel.Id);

            return RedirectToAction("Index");
        }

        private async Task<IList<LanguageViewModel>> GetLanguagesCollectionAsync()
        {
            IEnumerable<LanguageDto> languagessDto = await _infoService.GetLanguagesListAsync();
            IList<LanguageViewModel> languages = _dataAdapter.Map<IList<LanguageViewModel>>(languagessDto);

            return languages;
        }

        private async Task<IEnumerable<GroupViewModel>> GetGroupsCollectionAsync(GroupsListOptionsModel modelOptions)
        {
            IEnumerable<GroupDto> groupsDto = await _groupService.GetListAsync(_queryOptionsAdapter.Map(modelOptions));
            IEnumerable<GroupViewModel> groups = _dataAdapter.Map<IEnumerable<GroupViewModel>>(groupsDto);

            return groups;
        }
    }
}
