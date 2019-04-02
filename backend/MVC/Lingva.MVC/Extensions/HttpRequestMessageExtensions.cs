using Lingva.MVC.Models.Contracts;
using Lingva.MVC.Models.Response;
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
            FillParameters(parameters, allParameters);

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

        private static void FillParameters(Dictionary<string, object> dictSourse, Dictionary<string, object> dictTarget, string parent = "")
        {
            foreach (var param in dictSourse)
            {
                var paramValue = param.Value;
                if (paramValue != null)
                {
                    if(paramValue is IEnumerable<object>)
                    {
                        int i = 0;
                        foreach(var paramValueEnum in (IEnumerable<object>)paramValue)
                        {
                            if (paramValueEnum is IHttpParametersSource)
                            {
                                FillParameters((paramValueEnum as IHttpParametersSource).GetParametersDictionary(), dictTarget, parent + param.Key + "[" + i + "]" + ".");
                            }
                            else
                            {
                                dictTarget[parent + param.Key] = paramValue;
                            }

                            i++;
                        }
                    }
                    else
                    {
                        if (paramValue is IHttpParametersSource)
                        {
                            FillParameters((paramValue as IHttpParametersSource).GetParametersDictionary(), dictTarget, parent + param.Key + ".");
                        }
                        else
                        {
                            dictTarget[parent + param.Key] = paramValue;
                        }
                    }
                }
            }
        }
    }
}