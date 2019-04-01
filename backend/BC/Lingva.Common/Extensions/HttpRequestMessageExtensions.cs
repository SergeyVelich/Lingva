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

        //public static HttpRequestMessage AddParameters(this HttpRequestMessage request, Dictionary<string, object> parameters)
        //{
        //    bool isFirst = true;
        //    string parametersPatch = parameters.Count > 0 ? "?" : "";
        //    foreach (var param in parameters)
        //    {
        //        var paramValue = param.Value;
        //        if (paramValue is Dictionary<string, object>)
        //        {
        //            foreach (var paramInner in (Dictionary<string, object>)paramValue)
        //            {
        //                if()
        //                if (!isFirst)
        //                {
        //                    parametersPatch += "&";
        //                }
        //                isFirst = false;
        //                parametersPatch += param.Key + "." + paramInner.Key + "=" + paramInner.Value;
        //            }
        //        }
        //        else
        //        {
        //            if (!isFirst)
        //            {
        //                parametersPatch += "&";
        //            }
        //            isFirst = false;
        //            parametersPatch += param.Key + "=" + paramValue;
        //        }
        //    }

        //    request.RequestUri = new Uri(request.RequestUri.ToString() + parametersPatch);
        //    return request;
        //}

        public static HttpRequestMessage AddParameters(this HttpRequestMessage request, Dictionary<string, object> parameters)
        {
            string parametersPatch = parameters.Count > 0 ? "?" : "";

            Dictionary<string, object> allParameters = new Dictionary<string, object>();
            FillParameters(parameters, allParameters);


            bool isFirst = true;
            
            foreach (var param in allParameters)
            {
                if (!isFirst)
                {
                    parametersPatch += "&";
                }
                isFirst = false;
                parametersPatch += param.Key + "=" + param.Value;
            }

            request.RequestUri = new Uri(request.RequestUri.ToString() + parametersPatch);
            return request;
        }

        private static void FillParameters(Dictionary<string, object> dictSourse, Dictionary<string, object> dictTarget, string parent = "")
        {
            foreach (var param in dictSourse)
            {
                var paramValue = param.Value;
                if (paramValue is Dictionary<string, object>)
                {
                    FillParameters((Dictionary<string, object>)paramValue, dictTarget, param.Key + ".");
                }
                else
                {
                    if(param.Value != null)
                    {
                        dictTarget[parent + param.Key] = param.Value;
                    }                   
                }
            }
        }
    }
}