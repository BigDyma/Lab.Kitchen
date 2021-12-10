using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Kitchen
{
    public class HandleExceptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HandleExceptionsMiddleware> _logger;
        public HandleExceptionsMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
