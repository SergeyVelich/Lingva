using Lingva.MVC.Infrastructure.Exceptions;
using Lingva.MVC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace Lingva.MVC.Filters
{
    public class GlobalExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            string msgForUser;
            string msgForLog;
            //string stack = null;

            if (context.Exception is LingvaCustomException)
            {
                msgForUser = context.Exception.Message;
                msgForLog = context.Exception.Message;

                context.Exception = null;               
            }
            else
            {
                msgForUser = "An unhandled error occurred.";                    
                msgForLog = context.Exception.Message;
            }

            _logger.LogError(0, context.Exception, msgForLog);

            ErrorViewModel errorViewModel = new ErrorViewModel(msgForUser);

            context.ExceptionHandled = true;
            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";
            context.Result = new JsonResult(errorViewModel);
            //context.Result = new ViewResult { ViewName = "Error" };
        }
    }
}
