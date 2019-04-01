using Lingva.BC.Common.Enums;
using Lingva.Common.Extensions;
using Lingva.Common.Mapping;
using Lingva.MVC.Models.Request;
using Lingva.MVC.Models.Response;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Lingva.MVC.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {
        private readonly IDataAdapter _dataAdapter;
        private readonly ILogger<GroupController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly HttpClient _client;

        public GroupController(IDataAdapter dataAdapter, ILogger<GroupController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _dataAdapter = dataAdapter;
            _httpClientFactory = httpClientFactory;

            _client = _httpClientFactory.CreateClient();
            _client.BaseAddress = new Uri("http://localhost:6001/api");
        }

        // GET: group    
        public async Task<IActionResult> Index([FromQuery] FiltersViewModel filters, int page = 1, SortState sortOrder = SortState.NameAsc)
        {
            int pageSize = 3;

            IEnumerable<GroupViewModel> groupsViewModel;
            List<LanguageViewModel> languages;

            HttpRequestMessage request = await GetRequestAsync(HttpMethod.Get, "group");
            Dictionary<string, object> parameters = new Dictionary<string, object>()
                { { "filters", filters.GetPropertyAsDictionary() },
                  { "page", page },
                  { "sortOrder", sortOrder } };
            request.AddParameters(parameters);
            HttpResponseMessage response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                groupsViewModel = await response.Content.ReadAsAsync<IEnumerable<GroupViewModel>>();
            }
            else
            {
                groupsViewModel = Array.Empty<GroupViewModel>();
            }

            request = await GetRequestAsync(HttpMethod.Get, "info/languages");
            response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                languages = await response.Content.ReadAsAsync<List<LanguageViewModel>>();
            }
            else
            {
                languages = new List<LanguageViewModel>();
            }
            
            IndexViewModel viewModel = new IndexViewModel
            {
                //PageViewModel = new PageViewModel(count, page, pageSize),//??
                PageViewModel = new PageViewModel(4, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(languages, filters),
                Groups = groupsViewModel,
            };

            return View(viewModel);
        }

        // GET: group/get?id=2
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {            
            HttpRequestMessage request = await GetRequestAsync(HttpMethod.Get, "group/get");
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "id", id } };
            request.AddParameters(parameters);
            HttpResponseMessage response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            GroupViewModel groupViewModel = await response.Content.ReadAsAsync<GroupViewModel>();

            return View(groupViewModel);
        }

        // GET: group/create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: group/create
        [HttpPost]
        public async Task<IActionResult> Create(GroupCreateViewModel groupCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            HttpRequestMessage request = await GetRequestAsync(HttpMethod.Post, "group/create");
            string parametersString = JsonConvert.SerializeObject(groupCreateViewModel);
            StringContent content = new StringContent(parametersString, Encoding.UTF8, "application/json");
            request.AddBody(content);
            HttpResponseMessage response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            return Redirect("/Group/Index");
        }

        // GET: group/update?id=2
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {            
            HttpRequestMessage request = await GetRequestAsync(HttpMethod.Get, "group/get");
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "id", id } };
            request.AddParameters(parameters);          
            HttpResponseMessage response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            GroupCreateViewModel groupCreateViewModel = await response.Content.ReadAsAsync<GroupCreateViewModel>();

            return View(groupCreateViewModel);
        }

        // POST: group/update
        [HttpPost]
        public async Task<IActionResult> Update(GroupCreateViewModel groupCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            HttpRequestMessage request = await GetRequestAsync(HttpMethod.Put, "group/update");
            string parametersString = JsonConvert.SerializeObject(groupCreateViewModel);
            StringContent content = new StringContent(parametersString, Encoding.UTF8, "application/json");
            request.AddBody(content);
            HttpResponseMessage response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            return Redirect("/Group/Index");
        }

        // POST: group/delete?id=2
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            HttpRequestMessage request = await GetRequestAsync(HttpMethod.Delete, "group/delete");
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "id", id } };
            request.AddParameters(parameters);
            HttpResponseMessage response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            return Redirect("/Group/Index");
        }

        private async Task<HttpRequestMessage> GetRequestAsync(HttpMethod method, string requestUri = "")
        {
            string accessToken = await HttpContext.GetTokenAsync("access_token");
            string hostPatch = _client.BaseAddress.ToString();
            string requestPatch = (string.IsNullOrWhiteSpace(requestUri) ? "" : "/") + requestUri;

            HttpRequestMessage request = new HttpRequestMessage(method, hostPatch + requestPatch);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            return request;
        }
    }
}
