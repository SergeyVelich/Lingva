using Lingva.Common.Mapping;
using Lingva.MVC.ViewModel.Request;
using Lingva.MVC.ViewModel.Response;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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

        public GroupController(IDataAdapter dataAdapter, ILogger<GroupController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _dataAdapter = dataAdapter;
            _httpClientFactory = httpClientFactory;
        }

        // GET: group
        public async Task<IActionResult> Index()
        {
            IEnumerable<GroupViewModel> groupsViewModel;

            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:6001/api/group");
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                groupsViewModel = await response.Content.ReadAsAsync<IEnumerable<GroupViewModel>>();
            }
            else
            {
                groupsViewModel = Array.Empty<GroupViewModel>();
            }

            return View(groupsViewModel);
        }

        // GET: group/get?id=2
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:6001/api/group/get?"
                +"id=" + id);
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request);

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

            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:6001/api/group/create");
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var parametersString = JsonConvert.SerializeObject(groupCreateViewModel);
            request.Content = new StringContent(parametersString, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request);

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
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:6001/api/group/get?"
                + "id=" + id);
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request);

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

            var request = new HttpRequestMessage(HttpMethod.Put, "http://localhost:6001/api/group/update");
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var parametersString = JsonConvert.SerializeObject(groupCreateViewModel);
            request.Content = new StringContent(parametersString, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            return Redirect("/Group/Index");
        }

        // POST: groups/delete?id=2
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var request = new HttpRequestMessage(HttpMethod.Delete, "http://localhost:6001/api/group/delete?"
                + "id=" + id);
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            return Redirect("/Group/Index");
        }
    }
}
