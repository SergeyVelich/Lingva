using Lingva.BC.API.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lingva.BC.API.Services
{
    public class LingvaAPIService : ILingvaAPIService
    {
        public readonly HttpClient _client;
        private readonly HttpContext _httpContext;

        public LingvaAPIService(HttpClient client, HttpContext httpContext)
        {
            _httpContext = httpContext;

            client.BaseAddress = new Uri("http://localhost:6001/api");

            _client = client;
        }

        public async Task<HttpRequestMessage> GetRequestAsync(HttpMethod method, string requestUri)
        {
            HttpRequestMessage request = new HttpRequestMessage(method, _client.BaseAddress + requestUri);
            //var accessToken = await HttpContext.GetTokenAsync("access_token");
            //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            return request;
        }
    }
}

