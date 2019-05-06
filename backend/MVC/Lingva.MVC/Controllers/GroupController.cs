using Lingva.Common.Mapping;
using Lingva.MVC.Extensions;
using Lingva.MVC.Infrastructure.Exceptions;
using Lingva.MVC.Models.Entities;
using Lingva.MVC.Models.Group;
using Lingva.MVC.Models.Group.Index;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Lingva.MVC.Controllers
{
    //[Authorize]
    [ResponseCache(CacheProfileName = "NoCashing")]
    public class GroupController : Controller
    {
        private readonly IDataAdapter _dataAdapter;
        private readonly ILogger<GroupController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _memoryCache;

        private readonly HttpClient _client;

        public GroupController(IDataAdapter dataAdapter, ILogger<GroupController> logger, IHttpClientFactory httpClientFactory, IMemoryCache memoryCache)
        {
            _logger = logger;
            _dataAdapter = dataAdapter;
            _httpClientFactory = httpClientFactory;
            _memoryCache = memoryCache;

            _client = _httpClientFactory.CreateClient();
            _client.BaseAddress = new Uri("http://localhost:6001/api");
        }

        // GET: group    
        public async Task<IActionResult> Index(GroupsListOptionsModel modelOptions)
        {
            IEnumerable<GroupViewModel> groups = await GetGroupsCollectionAsync();
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

            HttpRequestMessage request = await GetRedirectRequestWithParametersAsync(HttpMethod.Get, "group/get");
            HttpResponseMessage response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new LingvaCustomException("Connection with app is broken");
            }

            GroupViewModel groupViewModel = await response.Content.ReadAsAsync<GroupViewModel>();
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

            HttpRequestMessage request = await GetRedirectRequestWithParametersAsync(HttpMethod.Post, "group/create");
            request.AddBody(groupViewModel);
            HttpResponseMessage response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

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

            HttpRequestMessage request = await GetRedirectRequestWithParametersAsync(HttpMethod.Get, "group/get");       
            HttpResponseMessage response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            GroupViewModel groupViewModel = await response.Content.ReadAsAsync<GroupViewModel>();
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

            HttpRequestMessage request = await GetRedirectRequestWithParametersAsync(HttpMethod.Put, "group/update");
            request.AddBody(groupViewModel);
            HttpResponseMessage response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

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

            HttpRequestMessage request = await GetRedirectRequestWithParametersAsync(HttpMethod.Get, "group/get");
            HttpResponseMessage response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            GroupViewModel groupViewModel = await response.Content.ReadAsAsync<GroupViewModel>();
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
            
            HttpRequestMessage request = await GetRedirectRequestWithParametersAsync(HttpMethod.Delete, "group/delete?id=" + groupViewModel.Id);
            request.AddBody(groupViewModel);
            HttpResponseMessage response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        private async Task<HttpRequestMessage> GetRedirectRequestWithParametersAsync(HttpMethod method, string requestUri = "")
        {
            HttpRequestMessage request = await GetRedirectRequestAsync(method, requestUri); 
            request.RequestUri = new Uri(request.RequestUri.ToString() + this.HttpContext.Request.QueryString);

            return request;
        }

        private async Task<HttpRequestMessage> GetRedirectRequestAsync(HttpMethod method, string requestUri = "")
        {
            string accessToken = await HttpContext.GetTokenAsync("access_token");
            string hostPatch = _client.BaseAddress.ToString();
            string requestPatch = (string.IsNullOrWhiteSpace(requestUri) ? "" : "/") + requestUri;

            HttpRequestMessage request = new HttpRequestMessage(method, hostPatch + requestPatch);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            return request;
        }

        private async Task<IList<LanguageViewModel>> GetLanguagesCollectionAsync()
        {
            HttpRequestMessage request = await GetRedirectRequestAsync(HttpMethod.Get, "info/languages");
            HttpResponseMessage response = await _client.SendAsync(request);

            IList<LanguageViewModel> languages;

            if (response.IsSuccessStatusCode)
            {
                languages = await response.Content.ReadAsAsync<IList<LanguageViewModel>>();
            }
            else
            {
                languages = Array.Empty<LanguageViewModel>();
            }

            return languages;
        }

        private async Task<IEnumerable<GroupViewModel>> GetGroupsCollectionAsync()
        {
            HttpRequestMessage request = await GetRedirectRequestWithParametersAsync(HttpMethod.Get, "group");
            HttpResponseMessage response = await _client.SendAsync(request);

            IEnumerable<GroupViewModel> groups;

            if (response.IsSuccessStatusCode)
            {
                groups = await response.Content.ReadAsAsync<IEnumerable<GroupViewModel>>();
            }
            else
            {
                groups = Array.Empty<GroupViewModel>();
            }

            return groups;
        }
    }
}
