using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;

namespace Lingva.MVC.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class HttpRequestMessageExtensions
    {
        public static HttpRequestMessage AddBody(this HttpRequestMessage request, object contentObject)
        {
            if (contentObject == null)
            {
                throw new ArgumentNullException();
            }

            string parametersString = JsonConvert.SerializeObject(contentObject);
            StringContent content = new StringContent(parametersString, Encoding.UTF8, "application/json");
            request.Content = content;

            return request;
        }    
    }
}