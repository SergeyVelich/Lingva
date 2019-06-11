using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Lingva.MVC.Filters
{
    public class GlobalLoggingFilter : Attribute, IAsyncActionFilter
    {
        private readonly ILogger<GlobalLoggingFilter> _logger;

        public GlobalLoggingFilter(ILogger<GlobalLoggingFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            HttpRequest request = context.HttpContext.Request;
            string logString = request.Host.Value + request.Path + request.QueryString.Value;
            _logger.LogInformation(0, logString);

            await next();

            HttpResponse response = context.HttpContext.Response;
            logString = logString + " (" + response.StatusCode.ToString() + ")";
            _logger.LogInformation(0, logString);            
        }
    }
}
