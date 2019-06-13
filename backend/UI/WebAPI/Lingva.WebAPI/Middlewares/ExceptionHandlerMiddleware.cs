using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Lingva.WebAPI.Middlewares
{
    public sealed class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                try
                {
                    //Обработка ошибки, к примеру, просто можем логировать сообщение ошибки
                    _logger.LogError(0, exception, exception.Message);
                }
                catch (Exception innerException)
                {
                    _logger.LogError(0, innerException, "Ошибка обработки исключения");
                }
                // Если в коде обработки ошибки мы снова получили ошибку, то пробрасываем ее выше по цепочке Middleware
                throw;
            }
        }
    }
}
