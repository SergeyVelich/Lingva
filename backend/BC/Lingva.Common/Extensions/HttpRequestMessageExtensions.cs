using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

namespace Lingva.Common.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class HttpRequestMessageExtensions
    {
        public static HttpRequestMessage AddBody(this HttpRequestMessage request, StringContent content)
        {
            if (content != null)
            {
                request.Content = content;
            }

            return request;
        }

        public static HttpRequestMessage AddParameters(this HttpRequestMessage request, Dictionary<string, string> parameters)
        {
            string parametersPatch = parameters.Count > 0 ? "?" : "";
            foreach (var param in parameters)
            {
                parametersPatch += param.Key + "=" + param.Value;
            }

            request.RequestUri = new Uri(request.RequestUri.ToString() + parametersPatch);
            return request;
        }

    }
}