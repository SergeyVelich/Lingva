using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;

namespace Lingva.MVC.Extensions
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

        public static HttpRequestMessage AddParameters(this HttpRequestMessage request, Dictionary<string, object> parameters)
        {
            Dictionary<string, object> allParameters = new Dictionary<string, object>();
            foreach (var param in parameters)
            {
                var paramValue = param.Value;
                if (paramValue != null)
                {
                    allParameters[param.Key] = paramValue;
                }
            }

            StringBuilder parametersPatch = new StringBuilder();
            if(allParameters.Count > 0)
            {
                parametersPatch.Append("?");

                bool isFirst = true;

                foreach (var param in allParameters)
                {
                    if (!isFirst)
                    {
                        parametersPatch.Append("&");
                    }
                    isFirst = false;
                    parametersPatch.Append(param.Key);
                    parametersPatch.Append("=");
                    parametersPatch.Append(param.Value);
                }
            }

            parametersPatch.Insert(0, request.RequestUri);
            request.RequestUri = new Uri(parametersPatch.ToString());
            return request;
        }
    }
}